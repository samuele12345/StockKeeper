using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MyApp1.Data;
using MyApp1.Models; // importazione delle class cs nella cartella Models

namespace MyApp1.Controllers
{
    public class ItemsController : Controller // ogni controller eredita dalla classe controller
    {
        private readonly MyApp1Context _context; // il _ indica un campo privato della classe che contiene il contesto del database

        // Riceve il contesto tramite dependency injection.
        public ItemsController(MyApp1Context context)
        {
            _context = context;
        }

        // Legge in modo asincrono tutti gli elementi dal database.
        public async Task<IActionResult> Index()
        {
            // Recupera tutti gli item e carica subito anche la proprietà di navigazione SerNumber collegata a ciascun record.
            // quindi nella view o nel controller si possono usare direttamente i dati del serial number collegato.
            var item = await _context.Item.Include(s => s.SerNumber).ToListAsync();
            return View(item);
        }

        // metodo get
        public IActionResult Create()
        {
            return View();
        }

        // Metodo POST: riceve i dati del form e li associa all'oggetto Items.
        [HttpPost]
        // Bind limita le proprietà compilate con i dati del form alle sole Id, Name e Price.
        public async Task<IActionResult> Create([Bind("Id", "Name", "Price")] Items item)
        {
            // cntrolla con l'if se un item con name e price del bind esiste già nel database, in caso non lo inserisce
            bool itemExists = await _context.Item
            .AnyAsync(x => x.Name == item.Name && x.Price == item.Price);

            if (itemExists)
            {
                // se invece che "" inserisco un campo specifico come Name, poi invece che All in asp-validation-summary="All" nel Create.cshtml
                // posso inserire asp-validation-summary="Name"
                ModelState.AddModelError("", "An item with the same name and price already exists.");
                return View(item);
            }


            // Controlla se i dati ricevuti dal form sono validi secondo il modello.
            if (ModelState.IsValid)
            {
                var serNum = new SerialNumber();

                var random = new Random();

                int num = random.Next(10, 100);

                string str = item.Name.Substring(0, 3);

                serNum.Name = str + num;


                _context.SerialNumbers.Add(serNum);
                await _context.SaveChangesAsync();

                item.IdSerial = serNum.Id;
                // Aggiunge il nuovo item al contesto come record da inserire nel database.
                _context.Item.Add(item);
                // Salva in modo asincrono le modifiche e scrive davvero il nuovo record nel database.
                await _context.SaveChangesAsync();
                // Reindirizza l'utente alla pagina Index dopo il salvataggio.
                return RedirectToAction("Index");
            }
            // Se i dati non sono validi, riapre la view Create mostrando l'oggetto e gli eventuali errori.
            return View(item);
        }

        // action get
        public async Task<IActionResult> Edit(int id)
        {
            // FirstOrDefaultAsync cerca il primo item con l'Id richiesto e restituisce null se non lo trova.
            var item = await _context.Item.FirstOrDefaultAsync(x => x.Id == id);
            // apre la view relativa all'item con id corrispondente
            return View(item);
        }

        // action post
        [HttpPost]
        // Bind stabilisce quali proprietà del model possono essere riempite con i dati del form prima dell'aggiornamento nel database.
        // Bind collega all'oggetto item solo i valori Id, Name e Price inviati dal form.
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Price")] Items item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Item.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        // ricevendo gli stessi parametri il nome per il post deve essere diverso
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // FindAsync cerca l'item tramite chiave primaria Id in modo diretto.
            var item = await _context.Item.FindAsync(id);
            if(item != null)
            {
                _context.Item.Remove(item);
                await _context.SaveChangesAsync();
            }
            // in ogni caaso, sia se l'item sia null oppure no, lo user verrà ridiretto alla pagina Index
            return RedirectToAction("Index");
        }




        /*
        public IActionResult Overview() // IActionResult cntienen tutte le tipologie di possibili dati ritornabili
        {
            var item = new Items() { Name="Keyboard", isLogged=true};
            return View(item);
        }

        // Action parameters: actions utili a ricevere dati da diverse sorgenti: url, query e form
        // Il nome del parametro 'id' deve corrispondere al nome nella route {id?} o nella query string
        // Funziona con: /Items/Edit/3 (route param) oppure /Items/Edit?id=3 (query string)

        public IActionResult Edit(int id)
        {
            return Content("id: " + id);
        }

        [HttpPost]
        public IActionResult Submit(string name)
        {
            var item = new Items();
            if(name != null)
            {
                item.Name = name;
            }
            else
            {
                item.Name = "Inserire un valore";
            }
            
            item.isLogged = true;
            return View("Overview", item);
        }

        */
    }
}
