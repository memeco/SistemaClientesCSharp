using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaClientes.Data;
using SistemaClientes.Models;
using System.Threading.Tasks;

namespace SistemaClientes.Controllers
{
    // Controlador para a entidade Cliente
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Injeção de dependência do contexto do banco de dados
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ: Listar todos os clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cliente.ToListAsync());
        }

        // READ: Detalhes de um cliente específico
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.ID_Cliente == id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // CREATE: Exibir o formulário para criar um novo cliente
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Salvar o novo cliente no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // UPDATE: Exibir o formulário para editar um cliente existente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // UPDATE: Salvar as alterações do cliente no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.ID_Cliente) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ID_Cliente)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // DELETE: Exibir a confirmação para deletar um cliente
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.ID_Cliente == id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // DELETE: Confirmar e deletar o cliente do banco de dados
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar se um cliente existe
        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.ID_Cliente == id);
        }
    }
}
