using Dane;

namespace Logika
{
    public class KulaMagazyn : IMagazyn<Kula>
    {
        private IList<Kula> repo;

        public KulaMagazyn()
        {
            this.repo = new List<Kula>();
        }

        public void Dodaj(Kula obj)
        {
            if (obj == null)
                return;

            repo.Add(obj);
        }

        public void Usun(Kula obj)
        {
            if (obj == null)
                return;

            repo.Remove(obj);
        }

        public IList<Kula> PobierzWszystkie()
        {
            return repo;
        }
    }
}