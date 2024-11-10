using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILibrariesService librariesService)
        {
            _librariesService = librariesService;
            _booksService = booksService;
        }

        // Implement the functionalities below

        public async Task<IActionResult>AddBook(int libraryId,[FromBody] Book book)

        {
            var Library= await _librariesService.GetLibraryByIdAsync(libraryId);
            if(library== null)
            return NotFound();
            await _booksService.AddBookAsync(libraryId,book);
            return CreatedAtAction(nameof(GetBooks), new {libraryId},book};

        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(int libraryId)
        {
             var Library= await _librariesService.GetLibraryByIdAsync(libraryId);
            if(library== null)
            return NotFound();
            var books= await _booksService.GetBooksByLibraryIdAsync(libraryId);
            return Ok(books);
            
        }
    }
