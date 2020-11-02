using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP3973_Assignment2.Data;
using COMP3973_Assignment2.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace COMP3973_Assignment2.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        const string BASE_URL = "https://www.googleapis.com/books/v1/volumes?q=harry+potter/";

        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _clientFactory;

        public List<Book> Books { get; set; }

        public bool GetStudentsError { get; private set; }

        public BooksController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }



        public async Task<IActionResult> Index()

        {

            HttpClient client = new HttpClient();

            var request = "https://www.googleapis.com/books/v1/volumes?q=harry+potter";

            var response = await client.GetAsync(request);

            List<Book> books = new List<Book>();



            if (response.IsSuccessStatusCode)

            {

                var content = await response.Content.ReadAsStringAsync();



                dynamic myList = JObject.Parse(content);

                var items = myList.items;
                int i = 0;
               

                foreach (var item in items)

                {

                    Book newBook = new Book();


                    newBook.Id = i;
                    i += 1;

                    newBook.Title = item.volumeInfo.title;

                    newBook.SmallThumbnail = item.volumeInfo.imageLinks.smallThumbnail;

                    newBook.Authors = item.volumeInfo.authors.ToString();

                    newBook.Publisher = item.volumeInfo.publisher;

                    newBook.PublishedDate = item.volumeInfo.publishedDate;

                    newBook.Description = item.volumeInfo.description;

                    newBook.ISBN_10 = item.volumeInfo.industryIdentifiers[1].indentifier;



                    books.Add(newBook);

                }

            }

            else

            {

            }

            ViewBag.Books = books;

            return View();

        }

        public async Task<IActionResult> Details(string id)
        {

            
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            var request = "https://www.googleapis.com/books/v1/volumes?q=harry+potter";

            var response = await client.GetAsync(request);

            List<Book> books = new List<Book>();



            if (response.IsSuccessStatusCode)

            {

                var content = await response.Content.ReadAsStringAsync();



                dynamic myList = JObject.Parse(content);

                var items = myList.items;
                int i = 0;


                foreach (var item in items)

                {

                    Book newBook = new Book();


                    newBook.Id = i;
                    i += 1;

                    newBook.Title = item.volumeInfo.title;

                    newBook.SmallThumbnail = item.volumeInfo.imageLinks.smallThumbnail;

                    newBook.Authors = item.volumeInfo.authors.ToString();

                    newBook.Publisher = item.volumeInfo.publisher;

                    newBook.PublishedDate = item.volumeInfo.publishedDate;

                    newBook.Description = item.volumeInfo.description;

                    newBook.ISBN_10 = item.volumeInfo.industryIdentifiers[1].indentifier;



                    books.Add(newBook);

                }

            }

            else

            {

            }


            int a = Int32.Parse(id);
            Book book = books.ElementAt(a);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

       
    }
}

