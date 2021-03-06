﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sorte.console;

namespace sorte.console.Migrations
{
    [DbContext(typeof(SorteContext))]
    partial class SorteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.6.20312.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("sorte.console.EstatisticaMegasena", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataConcurso")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuantDez01")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez02")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez03")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez04")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez05")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez06")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez07")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez08")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez09")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez10")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez11")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez12")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez13")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez14")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez15")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez16")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez17")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez18")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez19")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez20")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez21")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez22")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez23")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez24")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez25")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez26")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez27")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez28")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez29")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez30")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez31")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez32")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez33")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez34")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez35")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez36")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez37")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez38")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez39")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez40")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez41")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez42")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez43")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez44")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez45")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez46")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez47")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez48")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez49")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez50")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez51")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez52")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez53")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez54")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez55")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez56")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez57")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez58")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez59")
                        .HasColumnType("int");

                    b.Property<int>("QuantDez60")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EstatisticaMegasenas");
                });

            modelBuilder.Entity("sorte.console.Megasena", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acumulado")
                        .HasColumnType("char(3)");

                    b.Property<double?>("AcumuladoVirada")
                        .HasColumnType("float");

                    b.Property<double?>("ArredacaoTotal")
                        .HasColumnType("float");

                    b.Property<int>("Concurso")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataConcurso")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<double?>("EstimativaPremio")
                        .HasColumnType("float");

                    b.Property<int?>("Ganhadores")
                        .HasColumnType("int");

                    b.Property<int?>("GanhadoresQuadra")
                        .HasColumnType("int");

                    b.Property<int?>("GanhadoresQuina")
                        .HasColumnType("int");

                    b.Property<double?>("Rateio")
                        .HasColumnType("float");

                    b.Property<double?>("RateioQuadra")
                        .HasColumnType("float");

                    b.Property<double?>("RateioQuina")
                        .HasColumnType("float");

                    b.Property<double?>("ValorAcumulado")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Megasenas");
                });

            modelBuilder.Entity("sorte.console.MegasenaCidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("MegasenaId")
                        .HasColumnType("int");

                    b.Property<string>("UF")
                        .HasColumnType("char(2)");

                    b.HasKey("Id");

                    b.HasIndex("MegasenaId");

                    b.ToTable("MegasenaCidades");
                });

            modelBuilder.Entity("sorte.console.MegasenaCounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataConcurso")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Numero")
                        .HasColumnType("int");

                    b.Property<int?>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MegasenaCounters");
                });

            modelBuilder.Entity("sorte.console.Sorteados", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MegaSenaId")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("Ordem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MegaSenaId");

                    b.ToTable("Sorteados");
                });

            modelBuilder.Entity("sorte.console.MegasenaCidade", b =>
                {
                    b.HasOne("sorte.console.Megasena", "MegasenaConcurso")
                        .WithMany("Cidades")
                        .HasForeignKey("MegasenaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sorte.console.Sorteados", b =>
                {
                    b.HasOne("sorte.console.Megasena", "MegaSena")
                        .WithMany("NumerosSorteados")
                        .HasForeignKey("MegaSenaId");
                });
#pragma warning restore 612, 618
        }
    }
}
