using System;

namespace BibliothequeDigitale
{
  
    public class Magazine : Document
    {
        public int Numero { get; set; }

        public Magazine(Guid id, string titre, string auteur, int annee, int numero)
            : base(id, titre, auteur, annee)
        {
            Numero = numero;
        }

        public override void AfficherDetails()
        {
            Console.WriteLine("=== MAGAZINE ===");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Titre: {Titre}");
            Console.WriteLine($"Auteur: {Auteur}");
            Console.WriteLine($"Année: {Annee}");
            Console.WriteLine($"Numéro: {Numero}");
            Console.WriteLine();
        }
    }
}


