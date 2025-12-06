using System;
using System.Collections.Generic;

namespace BibliothequeDigitale
{
    class Program
    {
        static void Main(string[] args)
        {
            Bibliotheque bibliotheque = new Bibliotheque();
            bool continuer = true;

            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  BIBLIOTHÈQUE NUMÉRIQUE - SYSTÈME     ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            while (continuer)
            {
                AfficherMenu();
                string choix = Console.ReadLine()?.Trim() ?? "";

                try
                {
                    switch (choix)
                    {
                        case "1":
                            AjouterDocument(bibliotheque);
                            break;
                        case "2":
                            AfficherTous(bibliotheque);
                            break;
                        case "3":
                            Rechercher(bibliotheque);
                            break;
                        case "4":
                            SupprimerDocument(bibliotheque);
                            break;
                        case "5":
                            Sauvegarder(bibliotheque);
                            break;
                        case "6":
                            Charger(bibliotheque);
                            break;
                        case "7":
                            continuer = false;
                            Console.WriteLine("Au revoir !");
                            break;
                        default:
                            Console.WriteLine("❌ Option invalide. Veuillez choisir un nombre entre 1 et 7.");
                            Console.WriteLine();
                            break;
                    }
                }
                catch (DocumentNonTrouveException ex)
                {
                    Console.WriteLine($"❌ Erreur: {ex.Message}");
                    Console.WriteLine();
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"❌ Erreur: Fichier non trouvé - {ex.Message}");
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"❌ Erreur de format: {ex.Message}");
                    Console.WriteLine();
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"❌ Erreur d'entrée/sortie: {ex.Message}");
                    Console.WriteLine();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"❌ Erreur d'argument: {ex.Message}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erreur inattendue: {ex.Message}");
                    Console.WriteLine($"   Type: {ex.GetType().Name}");
                    Console.WriteLine();
                }
            }
        }

        static void AfficherMenu()
        {
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine("MENU PRINCIPAL");
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine("1. Ajouter un document");
            Console.WriteLine("2. Afficher tous les documents");
            Console.WriteLine("3. Rechercher par mot-clé");
            Console.WriteLine("4. Supprimer un document");
            Console.WriteLine("5. Sauvegarder dans un fichier");
            Console.WriteLine("6. Charger depuis un fichier");
            Console.WriteLine("7. Quitter");
            Console.WriteLine("════════════════════════════════════════");
            Console.Write("Votre choix: ");
        }

        static void AjouterDocument(Bibliotheque bibliotheque)
        {
            Console.WriteLine();
            Console.WriteLine("=== AJOUTER UN DOCUMENT ===");
            Console.WriteLine("Type de document:");
            Console.WriteLine("1. Livre");
            Console.WriteLine("2. Magazine");
            Console.WriteLine("3. Document PDF");
            Console.Write("Choix: ");
            string typeChoix = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Titre: ");
            string titre = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(titre))
            {
                Console.WriteLine("❌ Le titre ne peut pas être vide.");
                Console.WriteLine();
                return;
            }

            Console.Write("Auteur: ");
            string auteur = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(auteur))
            {
                Console.WriteLine("❌ L'auteur ne peut pas être vide.");
                Console.WriteLine();
                return;
            }

            Console.Write("Année: ");
            if (!int.TryParse(Console.ReadLine(), out int annee))
            {
                Console.WriteLine("❌ Année invalide.");
                Console.WriteLine();
                return;
            }

            Guid id = Guid.NewGuid();
            Document? doc = null;

            switch (typeChoix)
            {
                case "1":
                    Console.Write("Nombre de pages: ");
                    if (!int.TryParse(Console.ReadLine(), out int nombrePages))
                    {
                        Console.WriteLine("❌ Nombre de pages invalide.");
                        Console.WriteLine();
                        return;
                    }
                    doc = new Livre(id, titre, auteur, annee, nombrePages);
                    break;

                case "2":
                    Console.Write("Numéro: ");
                    if (!int.TryParse(Console.ReadLine(), out int numero))
                    {
                        Console.WriteLine("❌ Numéro invalide.");
                        Console.WriteLine();
                        return;
                    }
                    doc = new Magazine(id, titre, auteur, annee, numero);
                    break;

                case "3":
                    Console.Write("Taille (Mo): ");
                    if (!double.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double taille))
                    {
                        Console.WriteLine("❌ Taille invalide.");
                        Console.WriteLine();
                        return;
                    }
                    doc = new DocumentPDF(id, titre, auteur, annee, taille);
                    break;

                default:
                    Console.WriteLine("❌ Type de document invalide.");
                    Console.WriteLine();
                    return;
            }

            bibliotheque.AjouterDocument(doc);
            Console.WriteLine("✅ Document ajouté avec succès !");
            Console.WriteLine();
        }

        static void AfficherTous(Bibliotheque bibliotheque)
        {
            Console.WriteLine();
            bibliotheque.AfficherTous();
        }

        static void Rechercher(Bibliotheque bibliotheque)
        {
            Console.WriteLine();
            Console.WriteLine("=== RECHERCHER PAR MOT-CLÉ ===");
            Console.Write("Mot-clé: ");
            string motCle = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(motCle))
            {
                Console.WriteLine("❌ Le mot-clé ne peut pas être vide.");
                Console.WriteLine();
                return;
            }

            try
            {
                List<Document> resultats = bibliotheque.Rechercher(motCle);
                Console.WriteLine();
                Console.WriteLine($"✅ {resultats.Count} document(s) trouvé(s):");
                Console.WriteLine();
                foreach (var doc in resultats)
                {
                    doc.AfficherDetails();
                }
            }
            catch (DocumentNonTrouveException)
            {
                throw; 
            }
        }

        static void SupprimerDocument(Bibliotheque bibliotheque)
        {
            Console.WriteLine();
            Console.WriteLine("=== SUPPRIMER UN DOCUMENT ===");
            Console.Write("ID du document (Guid): ");
            string idString = Console.ReadLine()?.Trim() ?? "";

            if (!Guid.TryParse(idString, out Guid id))
            {
                Console.WriteLine("❌ ID invalide. Format attendu: Guid.");
                Console.WriteLine();
                return;
            }

            try
            {
                bibliotheque.SupprimerDocument(id);
                Console.WriteLine("✅ Document supprimé avec succès !");
                Console.WriteLine();
            }
            catch (DocumentNonTrouveException)
            {
                throw; 
            }
        }

        static void Sauvegarder(Bibliotheque bibliotheque)
        {
            Console.WriteLine();
            Console.WriteLine("=== SAUVEGARDER ===");
            Console.Write("Chemin du fichier (ex: bibliotheque.csv): ");
            string cheminFichier = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(cheminFichier))
            {
                Console.WriteLine("❌ Le chemin du fichier ne peut pas être vide.");
                Console.WriteLine();
                return;
            }

            try
            {
                bibliotheque.Sauvegarder(cheminFichier);
                Console.WriteLine($"✅ Bibliothèque sauvegardée avec succès dans: {cheminFichier}");
                Console.WriteLine();
            }
            catch (IOException)
            {
                throw; 
            }
            catch (ArgumentException)
            {
                throw; 
            }
        }

        static void Charger(Bibliotheque bibliotheque)
        {
            Console.WriteLine();
            Console.WriteLine("=== CHARGER ===");
            Console.Write("Chemin du fichier (ex: bibliotheque.csv): ");
            string cheminFichier = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(cheminFichier))
            {
                Console.WriteLine("❌ Le chemin du fichier ne peut pas être vide.");
                Console.WriteLine();
                return;
            }

            try
            {
                bibliotheque.Charger(cheminFichier);
                Console.WriteLine($"✅ Bibliothèque chargée avec succès depuis: {cheminFichier}");
                Console.WriteLine();
            }
            catch (FileNotFoundException)
            {
                throw; 
            }
            catch (FormatException)
            {
                throw; 
            }
            catch (IOException)
            {
                throw; 
            }
            catch (ArgumentException)
            {
                throw; 
            }
            catch (Exception)
            {
                throw; 
            }
        }
    }
}



