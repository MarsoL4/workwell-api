using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Repositories.EmpresaOrganizacao
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly WorkWellDbContext _context;

        public EmpresaRepository(WorkWellDbContext context)
        {
            _context = context;
        }

        public async Task<Empresa?> GetByIdAsync(long id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task AddAsync(Empresa empresa)
        {
            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
            }
        }
    }
}