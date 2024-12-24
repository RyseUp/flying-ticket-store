using Application.DTOs;
using Application.IRepository;
using Application.IService;
using Application.IUnitOfWork;
using Domain.Entities;
using Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookMapper _bookMapper;
        public BookService(BookMapper _mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookMapper = _mapper;
        }

        public async Task<BookDTO> Add(BookDTO bookDTO)
        {
            var book = _bookMapper.ToEntity(bookDTO);
            await _unitOfWork.Books.AddAsync(book);
            bookDTO = _bookMapper.ToDTO(await _unitOfWork.Books.GetByIdAsync(book.Id));
            return bookDTO;
        }

        public async Task Delete(int bookId)
        {
            await _unitOfWork.Books.DeleteAsync(bookId);
        }

        public async Task<BookDTO?> Get(int bookId)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookId);
            if (book == null) return null;
            return _bookMapper.ToDTO(book);
        }

        public async Task<ICollection<Book>> GetAll()
        {
            return await _unitOfWork.Books.GetAllAsync();
        }

        public async Task Update(BookDTO bookDTO)
        {
            var book = _bookMapper.ToEntity(bookDTO);
            await _unitOfWork.Books.UpdateAsync(book);
        }

        public async Task<ICollection<BookDTO>> Find()
        {
            //Expression<Func<Book, bool>> filter = b => b.Price > 100;

            //var expensiveBooks = await _unitOfWork.Books.(filter);
            //return _bookMapper.ToListDTO(expensiveBooks);
            return null;
        }

        public async Task<ICollection<BookDTO>> GetAllFullInfo()
        {
            var books = await _unitOfWork.Books.GetAll();
            return _bookMapper.ToListDTO(books);
        }

        public async Task<BookDTO?> GetFullInfo(int bookId)
        {
            var book = await _unitOfWork.Books.Get(bookId);
            if (book == null) return null;
            return _bookMapper.ToDTO(book);
        }

        public async Task<ICollection<BookDTO>> SearchName(string name)
        {
            if (string.IsNullOrEmpty(name)) name = "";
            var books = await _unitOfWork.Books.SearchName(name);
            return _bookMapper.ToListDTO(books);
        }


    }
}
