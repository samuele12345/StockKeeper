# StockPilot

A simple warehouse management app built with ASP.NET Core MVC and .NET 9.

## What is this?

StockPilot helps you keep track of your inventory items. You can add products, assign them categories, and the system automatically generates serial numbers for each item. It's straightforward and gets the job done.

## What you can do

- Add, edit, and delete products
- Organize items by category
- Automatic serial number generation (takes the first 3 letters of the product name + a random number)
- View all your inventory with associated serial numbers and categories
- Responsive interface that works on desktop and mobile

## Built with

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core for database stuff
- SQL Server LocalDB (for development)
- Bootstrap for styling

## Database structure

The app uses four main tables:

- **Items**: Your products (name, price, category, serial number reference)
- **SerialNumber**: Unique IDs for each item
- **Category**: Product categories
- **Persone**: User data

Items are linked to serial numbers (one-to-one) and categories (many-to-one).

## How to run it

1. Clone this repo
2. Open `MyApp1.slnx` in Visual Studio
3. The database connection is already set up for LocalDB in `appsettings.Development.json`
4. Run migrations to create the database:
   ```
   Update-Database
   ```
   (in Package Manager Console)
5. Hit F5 to run

That's it. The app will create the database automatically.

## Project structure

```
StockKeeper/
├── MyApp1/
│   ├── Controllers/     - MVC controllers
│   ├── Models/          - Data models (Items, Category, etc.)
│   ├── Views/           - Razor views
│   ├── Data/            - Database context
│   ├── Migrations/      - EF migrations
│   └── wwwroot/         - CSS, JS, images
└── README.md
```

## Technical notes

**Serial number generation**: When you create a new item, the code takes the first 3 characters of the name and adds a random 2-digit number. For example, "Keyboard" becomes "KEY42".

**Database queries**: The app uses eager loading with `.Include()` to avoid the N+1 query problem. This means all related data (serial numbers, categories) is loaded in a single database call instead of making separate queries for each item.

**Security**: Controllers use the `[Bind]` attribute to prevent over-posting attacks. This limits which properties can be set from form data.

## Configuration

For local development, the app uses LocalDB:
```json
"ConnectionStrings": {
  "DefaultConnectionString": "Server=(localdb)\\mssqllocaldb;Database=DBProva;..."
}
```

For production, just update the connection string in `appsettings.json` to point to your SQL Server instance.

## Things I might add later

- User login and permissions
- Search and filter options
- Export to Excel/PDF
- A dashboard with some stats
- Maybe barcode support

## About

Made by Samuele. This started as a learning project and evolved into something actually useful.

The project folder is still called "StockKeeper" and internally uses "MyApp1" in some places because I renamed it along the way. Works fine though.

## License

No specific license yet. If you want to use this, just ask.

