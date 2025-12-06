# Bibliothèque Numérique (Digital Library)

A .NET 6.0 console application for managing a digital library with support for different document types (Books, Magazines, and PDF documents). The application provides a menu-driven interface for adding, searching, deleting, and persisting documents to/from CSV files.

## 📋 Features

- 📚 **Multiple Document Types**: Support for Books (Livre), Magazines, and PDF Documents
- 🔍 **Search Functionality**: Search documents by title or author (case-insensitive)
- 💾 **Persistence**: Save and load library data from CSV files
- 🗑️ **Document Management**: Add and remove documents from the library
- 📋 **Display**: View all documents with detailed information
- ⚡ **Object-Oriented Design**: Uses inheritance, polymorphism, and encapsulation
- 🛡️ **Error Handling**: Comprehensive exception handling with custom exceptions

## 📦 Requirements

- .NET 6.0 SDK or later
- Windows, macOS, or Linux

## 🚀 Installation

1. Clone or download this repository
2. Navigate to the project directory:
   ```bash
   cd dotnet
   ```

3. Restore dependencies (if needed):
   ```bash
   dotnet restore
   ```

## 🔨 Building the Project

To build the project:
```bash
dotnet build
```

## ▶️ Running the Application

To run the application:
```bash
dotnet run
```

## 📖 Usage

When you run the application, you'll see a menu with the following options:

```
╔════════════════════════════════════════╗
║  BIBLIOTHÈQUE NUMÉRIQUE - SYSTÈME     ║
╚════════════════════════════════════════╝

════════════════════════════════════════
MENU PRINCIPAL
════════════════════════════════════════
1. Ajouter un document
2. Afficher tous les documents
3. Rechercher par mot-clé
4. Supprimer un document
5. Sauvegarder dans un fichier
6. Charger depuis un fichier
7. Quitter
════════════════════════════════════════
```

### Menu Options

#### 1. Ajouter un document (Add a Document)
Adds a new document to the library. You'll be prompted to choose:
- **Livre (Book)**: Requires title, author, year, and number of pages
- **Magazine**: Requires title, author, year, and issue number
- **Document PDF**: Requires title, author, year, and file size in MB

Each document is automatically assigned a unique GUID.

#### 2. Afficher tous les documents (Display All Documents)
Shows all documents currently in the library with their complete details.

#### 3. Rechercher par mot-clé (Search by Keyword)
Searches for documents matching a keyword in either the title or author field. The search is case-insensitive.

#### 4. Supprimer un document (Delete a Document)
Removes a document from the library by its GUID. You'll need to provide the document's ID.

#### 5. Sauvegarder dans un fichier (Save to File)
Saves all documents in the library to a CSV file. You'll be prompted for the file path (e.g., `bibliotheque.csv`).

#### 6. Charger depuis un fichier (Load from File)
Loads documents from a CSV file, replacing the current library contents. You'll be prompted for the file path.

#### 7. Quitter (Exit)
Exits the application.

## 📁 Project Structure

```
dotnet/
├── biblio.cs                    # Main library management class (Bibliotheque)
├── docs.cs                      # Abstract base class for documents (Document)
├── book.cs                      # Book document class (Livre)
├── Magazine.cs                  # Magazine document class
├── DocumentPDF.cs               # PDF document class
├── DocumentException.cs         # Custom exception class (DocumentNonTrouveException)
├── Program.cs                   # Main entry point and UI
├── BibliothequeDigitale.csproj  # Project file
└── README.md                    # This file
```

## 🏗️ Class Hierarchy

```
Document (abstract)
├── Livre (Book)
├── Magazine
└── DocumentPDF
```

### Document Properties
- `Id` (Guid): Unique identifier
- `Titre` (string): Document title
- `Auteur` (string): Author name
- `Annee` (int): Publication year

### Document-Specific Properties
- **Livre**: `NombrePages` (int) - Number of pages
- **Magazine**: `Numero` (int) - Issue number
- **DocumentPDF**: `TailleEnMo` (double) - File size in megabytes

## 📄 CSV File Format

The application saves and loads documents in CSV format. Each line represents one document:

```
Type,Id,Titre,Auteur,Annee,SpecificProperty
```

### Examples:

**Book:**
```
Livre,123e4567-e89b-12d3-a456-426614174000,Le Petit Prince,Antoine de Saint-Exupéry,1943,96
```

**Magazine:**
```
Magazine,223e4567-e89b-12d3-a456-426614174001,National Geographic,National Geographic Society,2023,456
```

**PDF Document:**
```
DocumentPDF,323e4567-e89b-12d3-a456-426614174002,Guide C#,Microsoft,2022,2.5
```

## 💡 Example Workflow

1. **Add Documents:**
   - Select option `1`
   - Choose document type (1, 2, or 3)
   - Enter document details
   - Document is added with a unique ID

2. **View Documents:**
   - Select option `2`
   - All documents are displayed

3. **Search:**
   - Select option `3`
   - Enter a keyword
   - Matching documents are displayed

4. **Save:**
   - Select option `5`
   - Enter filename (e.g., `my_library.csv`)
   - Library is saved to CSV

5. **Load:**
   - Select option `6`
   - Enter filename
   - Library is loaded from CSV

## ⚠️ Error Handling

The application includes comprehensive error handling:

- **DocumentNonTrouveException**: Thrown when a document is not found
- **FileNotFoundException**: Thrown when a file doesn't exist
- **FormatException**: Thrown when CSV format is invalid
- **IOException**: Thrown for file I/O errors
- **ArgumentException**: Thrown for invalid input arguments

All errors are displayed with user-friendly messages in French.

## 🎨 Design Patterns

- **Inheritance**: Document base class with derived types
- **Polymorphism**: Virtual methods for displaying document details
- **Encapsulation**: Private serialization methods
- **Exception Handling**: Custom exceptions for domain-specific errors

## 🛠️ Technologies Used

- **.NET 6.0**: Target framework
- **C#**: Programming language
- **LINQ**: For querying collections
- **File I/O**: For CSV persistence

## 📝 Code Structure

### Main Classes

- **Bibliotheque**: Manages the collection of documents, handles serialization/deserialization
- **Document**: Abstract base class defining common document properties
- **Livre**: Concrete class for book documents
- **Magazine**: Concrete class for magazine documents
- **DocumentPDF**: Concrete class for PDF documents
- **DocumentNonTrouveException**: Custom exception for document not found scenarios
- **Program**: Main entry point with console UI

## 🔧 Development

### Building from Source

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

### Running Tests

Currently, the project doesn't include unit tests. You can add test projects using:

```bash
dotnet new xunit -n BibliothequeDigitale.Tests
```

## 📄 License

This project is provided as-is for educational purposes.

## 👤 Author

Created as a .NET console application demonstrating object-oriented programming principles, file I/O, and exception handling.

---

**Note**: This is a console application that requires user interaction. Make sure to run it in a terminal that supports interactive input.

