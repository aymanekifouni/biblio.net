using System;

namespace BibliothequeDigitale
{
    /// <summary>
    /// Abstract base class representing a document in the digital library
    /// </summary>
    public abstract class Document
    {
        public Guid Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int Annee { get; set; }

        /// <summary>
        /// Parameterized constructor for Document
        /// </summary>
        protected Document(Guid id, string titre, string auteur, int annee)
        {
            Id = id;
            Titre = titre ?? throw new ArgumentNullException(nameof(titre));
            Auteur = auteur ?? throw new ArgumentNullException(nameof(auteur));
            Annee = annee;
        }

        /// <summary>
        /// Abstract method to display document details
        /// Must be implemented by derived classes
        /// </summary>
        public abstract void AfficherDetails();
    }
}

