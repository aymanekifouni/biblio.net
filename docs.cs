using System;

namespace BibliothequeDigitale
{
    
    public abstract class Document
    {
        public Guid Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int Annee { get; set; }

      
        protected Document(Guid id, string titre, string auteur, int annee)
        {
            Id = id;
            Titre = titre ?? throw new ArgumentNullException(nameof(titre));
            Auteur = auteur ?? throw new ArgumentNullException(nameof(auteur));
            Annee = annee;
        }

       
        public abstract void AfficherDetails();
    }
}


