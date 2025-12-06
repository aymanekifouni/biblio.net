using System;

namespace BibliothequeDigitale
{
    /// <summary>
    /// Concrete class representing a PDF document
    /// </summary>
    public class DocumentPDF : Document
    {
        public double TailleEnMo { get; set; }

        public DocumentPDF(Guid id, string titre, string auteur, int annee, double tailleEnMo)
            : base(id, titre, auteur, annee)
        {
            TailleEnMo = tailleEnMo;
        }

        public override void AfficherDetails()
        {
            Console.WriteLine("=== DOCUMENT PDF ===");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Titre: {Titre}");
            Console.WriteLine($"Auteur: {Auteur}");
            Console.WriteLine($"Année: {Annee}");
            Console.WriteLine($"Taille (Mo): {TailleEnMo:F2}");
            Console.WriteLine();
        }
    }
}

