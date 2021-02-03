export class Mitarbeiter {
    constructor(public id: number, public vorname: string,
        public nachname: string, public geburtsdatum: Date,
        public isAzubi: boolean) {
    }
}