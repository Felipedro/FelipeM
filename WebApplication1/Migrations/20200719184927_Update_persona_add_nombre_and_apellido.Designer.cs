﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(PersonaContexto))]
    [Migration("20200719184927_Update_persona_add_nombre_and_apellido")]
    partial class Update_persona_add_nombre_and_apellido
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication1.Models.Persona", b =>
                {
                    b.Property<int>("idPersona")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("apellido");

                    b.Property<string>("documento");

                    b.Property<string>("email");

                    b.Property<DateTime>("fechaNacimiento");

                    b.Property<string>("nombre");

                    b.Property<string>("pais");

                    b.Property<string>("sexo");

                    b.Property<string>("telefono");

                    b.Property<string>("tipoDocumento");

                    b.HasKey("idPersona");

                    b.ToTable("Persona");
                });
#pragma warning restore 612, 618
        }
    }
}
