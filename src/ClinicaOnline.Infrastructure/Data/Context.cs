using ClinicaOnline.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaOnline.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Parceiro> Parceiros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medico>(ConfigureMedico);
            modelBuilder.Entity<Paciente>(ConfigurePaciente);
            modelBuilder.Entity<Parceiro>(ConfigureParceiro);
            modelBuilder.Entity<Usuario>(ConfigureUsuario);
        }

        private void ConfigureMedico(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(medico => medico.Id);              

            builder.HasIndex(medico => new { medico.Crm, medico.UfCrm });         

            builder.Property(medico => medico.Nome)
                .HasMaxLength(255)
                .IsRequired();     

            builder.Property(cb => cb.Crm)
                .HasMaxLength(50)
                .IsRequired();   

            builder.Property(cb => cb.UfCrm)
                .HasColumnName("Uf_crm")
                .HasMaxLength(50)
                .IsRequired();  

            builder.Property(cb => cb.Especialidade)
                .HasMaxLength(255)
                .IsRequired();  
        }

        private void ConfigurePaciente(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(paciente => paciente.Id);              

            builder.HasIndex(paciente => paciente.Cpf);    

            builder.Property(paciente => paciente.Nome)
                .HasMaxLength(255);   

            builder.Property(paciente => paciente.Cpf)
                .HasMaxLength(11)
                .IsRequired();   

            builder.Property(paciente => paciente.Telefone)
                .HasMaxLength(20);

            builder.Property(paciente => paciente.Medico)
                .HasColumnName("MedicoId")
                .IsRequired();
        }

        private void ConfigureParceiro(EntityTypeBuilder<Parceiro> builder)
        {
            builder.HasKey(parceiro => parceiro.Id);         

            builder.Property(parceiro => parceiro.Nome)
                .HasMaxLength(255)
                .IsRequired();   

            builder.Property(parceiro => parceiro.ApiKey)
                .HasColumnName("Api_Key")
                .HasMaxLength(36)
                .IsRequired();
        }

        private void ConfigureUsuario(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(usuario => usuario.Id);            

            builder.HasIndex(usuario => usuario.Email);       

            builder.Property(usuario => usuario.Email)
                .HasMaxLength(255)
                .IsRequired();         

            builder.Property(usuario => usuario.Nome)
                .HasMaxLength(255)
                .IsRequired();       

            builder.Property(usuario => usuario.Senha)
                .HasMaxLength(255)
                .IsRequired();     

            builder.Property(usuario => usuario.Eperfil)
                .HasColumnName("Perfil")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}