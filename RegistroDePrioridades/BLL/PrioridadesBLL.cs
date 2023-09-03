using Microsoft.EntityFrameworkCore;
using RegistroDePrioridades.DAL;
using RegistroDePrioridades.Models;
using System.Linq.Expressions;

namespace RegistroDePrioridades.BLL
{
    public class PrioridadesBLL
    {
        private Contexto contexto;
        public PrioridadesBLL(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public bool Existe(int PrioridadId)
        {
            return contexto.Prioridades.Any(p => p.PrioridadId == PrioridadId);
        }
        public bool Insertar(Prioridades prioridades)
        {
            contexto.Prioridades.Add(prioridades);
            int insertado = contexto.SaveChanges();
            return insertado > 0;
        }
        public bool Modificar(Prioridades prioridades)
        {
            contexto.Update(prioridades);
            int modificado = contexto.SaveChanges();
            return modificado > 0;
        }
        public bool Guardar(Prioridades prioridades)
        {
            if (!Existe(prioridades.PrioridadId))
                return Insertar(prioridades);
            else
                return Modificar(prioridades);
        }
        public bool Eliminar(Prioridades prioridades)
        {
            contexto.Entry(prioridades).State = EntityState.Deleted;
            return contexto.SaveChanges() > 0;
        }
        public Prioridades? Buscar(int PrioridadId)
        {
            return contexto.Prioridades
                    .AsNoTracking()
                    .SingleOrDefault(p => p.PrioridadId == PrioridadId);
        }
        public List<Prioridades> Listar(Expression<Func<Prioridades, bool>> Criterio)
        {
            return contexto.Prioridades
                    .Where(Criterio)
                    .AsNoTracking()
                    .ToList();
        }

    }
}
