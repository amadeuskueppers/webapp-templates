using Ak.Template.DataClasses;
using System.Collections.Generic;

namespace Ak.Template.Logic.Contract
{
    public interface IMitarbeiterManager
    {
        IEnumerable<Mitarbeiter> GetAll();

        Mitarbeiter GetById(int id);

        Mitarbeiter Create(Mitarbeiter mitarbeiter);

        Mitarbeiter Modify(int id, Mitarbeiter mitarbeiter);

        void Remove(int id);
    }
}
