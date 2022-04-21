namespace Logika
{
    internal interface IMagazyn<Obj>
    {
        public void Dodaj(Obj obj);
        public void Usun(Obj obj);
        public IList<Obj> PobierzWszystkie();
    }
}
