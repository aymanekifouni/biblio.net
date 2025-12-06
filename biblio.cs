using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BibliothequeDigitale
{
    /// <summary>
    /// Class managing a collection of documents with save/load functionality
    /// </summary>
    public class Bibliotheque
    {
        private List<Document> documents;

        public Bibliotheque()
        {
            documents = new List<Document>();
        }

        /// <summary>
        /// Adds a document to the library
        /// </summary>
        public void AjouterDocument(Document d)
        {
            if (d == null)
                throw new ArgumentNullException(nameof(d));

            documents.Add(d);
        }

        /// <summary>
        /// Removes a document by its ID
        /// Throws DocumentNonTrouveException if document not found
        /// </summary>
        public void SupprimerDocument(Guid id)
        {
            var document = documents.FirstOrDefault(d => d.Id == id);
            if (document == null)
            {
                throw new DocumentNonTrouveException($"Aucun document avec l'ID {id} n'a été trouvé.");
            }

            documents.Remove(document);
        }

        /// <summary>
        /// Searches for documents by keyword in Title or Author
        /// Throws DocumentNonTrouveException if no documents found
        /// </summary>
        public List<Document> Rechercher(string motCle)
        {
            if (string.IsNullOrWhiteSpace(motCle))
                throw new ArgumentException("Le mot-clé ne peut pas être vide.", nameof(motCle));

            var resultats = documents.Where(d =>
                d.Titre.Contains(motCle, StringComparison.OrdinalIgnoreCase) ||
                d.Auteur.Contains(motCle, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (resultats.Count == 0)
            {
                throw new DocumentNonTrouveException($"Aucun document trouvé avec le mot-clé '{motCle}'.");
            }

            return resultats;
        }

        /// <summary>
        /// Displays all documents in the library
        /// </summary>
        public void AfficherTous()
        {
            if (documents.Count == 0)
            {
                Console.WriteLine("La bibliothèque est vide.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"=== TOUS LES DOCUMENTS ({documents.Count}) ===");
            foreach (var doc in documents)
            {
                doc.AfficherDetails();
            }
        }

        /// <summary>
        /// Saves all documents to a CSV file
        /// Uses FileStream and StreamWriter with proper resource cleanup
        /// </summary>
        public void Sauvegarder(string cheminFichier)
        {
            if (string.IsNullOrWhiteSpace(cheminFichier))
                throw new ArgumentException("Le chemin du fichier ne peut pas être vide.", nameof(cheminFichier));

            try
            {
                using (FileStream fileStream = new FileStream(cheminFichier, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    foreach (var doc in documents)
                    {
                        string ligne = SerializeDocument(doc);
                        writer.WriteLine(ligne);
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new IOException($"Le répertoire spécifié n'existe pas: {cheminFichier}", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new IOException($"Accès refusé au fichier: {cheminFichier}", ex);
            }
            catch (IOException ex)
            {
                throw new IOException($"Erreur lors de l'écriture du fichier: {cheminFichier}", ex);
            }
        }

        /// <summary>
        /// Loads documents from a CSV file
        /// Reads line by line and reconstructs the correct Document objects
        /// Uses proper resource cleanup
        /// </summary>
        public void Charger(string cheminFichier)
        {
            if (string.IsNullOrWhiteSpace(cheminFichier))
                throw new ArgumentException("Le chemin du fichier ne peut pas être vide.", nameof(cheminFichier));

            if (!File.Exists(cheminFichier))
            {
                throw new FileNotFoundException($"Le fichier n'existe pas: {cheminFichier}");
            }

            List<Document> documentsCharges = new List<Document>();
            int ligneNumero = 0;

            try
            {
                using (FileStream fileStream = new FileStream(cheminFichier, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string? ligne;
                    while ((ligne = reader.ReadLine()) != null)
                    {
                        ligneNumero++;
                        if (string.IsNullOrWhiteSpace(ligne))
                            continue;

                        try
                        {
                            Document? doc = DeserializeDocument(ligne);
                            if (doc != null)
                            {
                                documentsCharges.Add(doc);
                            }
                        }
                        catch (FormatException ex)
                        {
                            throw new FormatException($"Erreur de format à la ligne {ligneNumero}: {ex.Message}", ex);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Erreur lors du traitement de la ligne {ligneNumero}: {ex.Message}", ex);
                        }
                    }
                }

                // Replace existing documents with loaded ones
                documents = documentsCharges;
            }
            catch (FileNotFoundException)
            {
                throw; // Re-throw FileNotFoundException as-is
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new IOException($"Le répertoire spécifié n'existe pas: {cheminFichier}", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new IOException($"Accès refusé au fichier: {cheminFichier}", ex);
            }
            catch (IOException ex)
            {
                throw new IOException($"Erreur lors de la lecture du fichier: {cheminFichier}", ex);
            }
        }

        /// <summary>
        /// Serializes a Document to CSV format
        /// First column indicates document type
        /// </summary>
        private string SerializeDocument(Document doc)
        {
            if (doc is Livre livre)
            {
                return $"Livre,{livre.Id},{livre.Titre},{livre.Auteur},{livre.Annee},{livre.NombrePages}";
            }
            else if (doc is Magazine magazine)
            {
                return $"Magazine,{magazine.Id},{magazine.Titre},{magazine.Auteur},{magazine.Annee},{magazine.Numero}";
            }
            else if (doc is DocumentPDF pdf)
            {
                return $"DocumentPDF,{pdf.Id},{pdf.Titre},{pdf.Auteur},{pdf.Annee},{pdf.TailleEnMo}";
            }
            else
            {
                throw new NotSupportedException($"Type de document non supporté: {doc.GetType().Name}");
            }
        }

        /// <summary>
        /// Deserializes a CSV line to a Document object
        /// </summary>
        private Document? DeserializeDocument(string ligne)
        {
            if (string.IsNullOrWhiteSpace(ligne))
                return null;

            string[] colonnes = ligne.Split(',');
            if (colonnes.Length < 5)
            {
                throw new FormatException($"Ligne invalide: nombre de colonnes insuffisant ({colonnes.Length})");
            }

            string typeDocument = colonnes[0].Trim();

            if (!Guid.TryParse(colonnes[1].Trim(), out Guid id))
            {
                throw new FormatException($"ID invalide: {colonnes[1]}");
            }

            string titre = colonnes[2].Trim();
            string auteur = colonnes[3].Trim();

            if (!int.TryParse(colonnes[4].Trim(), out int annee))
            {
                throw new FormatException($"Année invalide: {colonnes[4]}");
            }

            switch (typeDocument)
            {
                case "Livre":
                    if (colonnes.Length < 6)
                        throw new FormatException("Ligne Livre incomplète: nombre de pages manquant");
                    if (!int.TryParse(colonnes[5].Trim(), out int nombrePages))
                        throw new FormatException($"Nombre de pages invalide: {colonnes[5]}");
                    return new Livre(id, titre, auteur, annee, nombrePages);

                case "Magazine":
                    if (colonnes.Length < 6)
                        throw new FormatException("Ligne Magazine incomplète: numéro manquant");
                    if (!int.TryParse(colonnes[5].Trim(), out int numero))
                        throw new FormatException($"Numéro invalide: {colonnes[5]}");
                    return new Magazine(id, titre, auteur, annee, numero);

                case "DocumentPDF":
                    if (colonnes.Length < 6)
                        throw new FormatException("Ligne DocumentPDF incomplète: taille manquante");
                    if (!double.TryParse(colonnes[5].Trim(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double taille))
                        throw new FormatException($"Taille invalide: {colonnes[5]}");
                    return new DocumentPDF(id, titre, auteur, annee, taille);

                default:
                    throw new FormatException($"Type de document inconnu: {typeDocument}");
            }
        }
    }
}

