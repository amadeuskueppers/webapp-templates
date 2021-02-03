using System;
using System.Linq;
using Ak.Template.DataClasses;
using Ak.Template.Logic.Contract;
using Ak.Template.Logic.Contract.Exceptions;
using System.Collections.Generic;

namespace Ak.Template.Logic
{
    public class MitarbeiterManager : IMitarbeiterManager
    {
        // todo: hier muss die Fake liste durch eine Referenz auf die Datenschicht / Repository erstetzt werden
        // Um wirklich sauber zu sein, sollte der wirkliche DB-Zugriff in einer eigenen Dll implementiert werden :)
        List<Mitarbeiter> _fakeRepositoryEntries;

        public MitarbeiterManager()
        {
            SetupFakeData();
        }

        public Mitarbeiter Create(Mitarbeiter mitarbeiter)
        {
            var largestFakeId = _fakeRepositoryEntries.Max(m => m.Id);
            var newFakeId = ++largestFakeId;

            mitarbeiter.Id = newFakeId;
            _fakeRepositoryEntries.Add(mitarbeiter);

            return mitarbeiter;
        }

        public IEnumerable<Mitarbeiter> GetAll()
        {
            var result = _fakeRepositoryEntries;

            return result;
        }

        public Mitarbeiter GetById(int id)
        {
            var result = _fakeRepositoryEntries.FirstOrDefault(m => m.Id == id);

            if(result == null)
            {
                throw new NotFoundException($"No entry with id '{id}' found.");
            }

            return result;
        }

        public Mitarbeiter Modify(int id, Mitarbeiter mitarbeiter)
        {
            var matchingEntry = GetById(id);

            // instanz mit alten Werten aus der collection entfernen
            _fakeRepositoryEntries.Remove(matchingEntry);

            // todo: das mapping der properties sollte der AutoMapper übernehmen!
            matchingEntry.Geburtsdatum = mitarbeiter.Geburtsdatum;
            matchingEntry.Vorname = mitarbeiter.Vorname;
            matchingEntry.Nachname = mitarbeiter.Nachname;
            matchingEntry.IsAzubi = mitarbeiter.IsAzubi;

            // aktualisierte instanz wieder einfuegen 
            _fakeRepositoryEntries.Add(matchingEntry);

            return matchingEntry;
        }

        public void Remove(int id)
        {
            var entry = GetById(id);
            _fakeRepositoryEntries.Remove(entry);
        }

        private void SetupFakeData()
        {
            _fakeRepositoryEntries = new List<Mitarbeiter>
            {
                new Mitarbeiter {Id = 1, Geburtsdatum = new DateTime(2000, 01, 01), Nachname = "Saubermann", Vorname = "Hans", IsAzubi = true },
                new Mitarbeiter {Id = 2, Geburtsdatum = new DateTime(1983, 03, 02), Nachname = "Müller", Vorname = "Johann", IsAzubi = false},
                new Mitarbeiter {Id = 3, Geburtsdatum = new DateTime(1977, 01, 02), Nachname = "Mustermann", Vorname = "Max", IsAzubi = false},
                new Mitarbeiter {Id = 4, Geburtsdatum = new DateTime(1977, 08, 02), Nachname = "Musterfrau", Vorname = "Sabine", IsAzubi = false},
            };
        }
    }
}
