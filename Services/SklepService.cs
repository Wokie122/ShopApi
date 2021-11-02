using AutoMapper;
using ShopApi.Entities;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Services
{
    public interface ISklepService
    {
        Artykul GetById(int id);
        IEnumerable<Artykul> GetAll(StronnicowanieDto stronnicowanie);
        int Create(CreateArtykulDto dto);
        public bool Delete(int id);
        public bool Update(int id, UpdateArtykulDto dto);
    }

    public class SklepService : ISklepService
    {
        private readonly SklepDbContext _dbContext;
        private readonly IMapper _mapper;

        public SklepService(SklepDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Update(int id, UpdateArtykulDto dto)
        {
            var artykul = _dbContext
                .Artykuly
                .FirstOrDefault(r => r.Id == id);

            if (artykul is null)
                return false;

            artykul.Name = dto.Name;
            artykul.Description = dto.Description;
            artykul.Price = dto.Price;

            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var artykul = _dbContext
             .Artykuly
             .FirstOrDefault(r => r.Id == id);

            if (artykul is null)
                return false;

            _dbContext.Artykuly.Remove(artykul);
            _dbContext.SaveChanges();
            return true;

        }

        public Artykul GetById(int id)
        {
            var artykul = _dbContext
                .Artykuly
                .FirstOrDefault(r => r.Id == id);

            if (artykul is null)
                return null;

            var result = _mapper.Map<Artykul>(artykul);
            return result;
        }

        public IEnumerable<Artykul> GetAll(StronnicowanieDto stronnicowanie)
        {
            var artykuly = _dbContext
                .Artykuly.Skip((stronnicowanie.Strona - 1) * stronnicowanie.Ilosc).Take(stronnicowanie.Ilosc)
                .ToList();

            return artykuly;
        }

        public int Create(CreateArtykulDto dto)
        {
            var artykuly = _mapper.Map<Artykul>(dto);
            _dbContext.Artykuly.Add(artykuly);
            _dbContext.SaveChanges();

            return artykuly.Id;
        }
    }
}
