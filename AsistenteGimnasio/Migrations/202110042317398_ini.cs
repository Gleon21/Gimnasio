namespace AsistenteGimnasio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Imagen = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Ejercicios",
                c => new
                    {
                        EjercicioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        CategoriaId = c.Int(nullable: false),
                        EnlaceVideo = c.String(),
                        Imagen = c.String(),
                        Duracion = c.String(),
                        Calorias = c.Int(nullable: false),
                        hidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EjercicioId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.IMCs",
                c => new
                    {
                        IMCId = c.Int(nullable: false, identity: true),
                        Maximo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Minimo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IMCId);
            
            CreateTable(
                "dbo.Notificacions",
                c => new
                    {
                        NotificacionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Fecha = c.String(),
                        Activa = c.Boolean(nullable: false),
                        Titulo = c.String(),
                        RedireccionUrl = c.String(),
                    })
                .PrimaryKey(t => t.NotificacionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Fecha_Registro = c.String(),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Sexo = c.String(),
                        Fecha_de_Nacimiento = c.String(),
                        Direccion = c.String(),
                        pss2 = c.String(),
                        Activo = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Recetas",
                c => new
                    {
                        RecetaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        TipoRecetaId = c.Int(nullable: false),
                        TiempoComidaId = c.Int(nullable: false),
                        Imagen = c.String(),
                        Procedimiento = c.String(),
                        Ingredientes = c.String(),
                        Calorias = c.String(),
                        HidratosDeCrabono = c.String(),
                        Proteinas = c.String(),
                        grasa = c.String(),
                    })
                .PrimaryKey(t => t.RecetaId)
                .ForeignKey("dbo.TiempoComidas", t => t.TiempoComidaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoRecetas", t => t.TipoRecetaId, cascadeDelete: true)
                .Index(t => t.TipoRecetaId)
                .Index(t => t.TiempoComidaId);
            
            CreateTable(
                "dbo.TiempoComidas",
                c => new
                    {
                        TiempoComidaId = c.Int(nullable: false, identity: true),
                        Tiempo = c.String(),
                    })
                .PrimaryKey(t => t.TiempoComidaId);
            
            CreateTable(
                "dbo.TipoRecetas",
                c => new
                    {
                        TipoRecetaId = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                    })
                .PrimaryKey(t => t.TipoRecetaId);
            
            CreateTable(
                "dbo.RegistroEjercicios",
                c => new
                    {
                        RegistroEjercicioId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        EjercicioId = c.Int(nullable: false),
                        HoraInicio = c.String(),
                        HoraFin = c.String(),
                        Tiempo = c.String(),
                        Fecha = c.String(),
                    })
                .PrimaryKey(t => t.RegistroEjercicioId)
                .ForeignKey("dbo.Ejercicios", t => t.EjercicioId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.EjercicioId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Saluds",
                c => new
                    {
                        SaludId = c.Int(nullable: false, identity: true),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estatura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(maxLength: 128),
                        FechadeRegistro = c.String(),
                        IMC = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SaludId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sesiones",
                c => new
                    {
                        SesionesId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Fecha = c.String(),
                        Hora = c.String(),
                    })
                .PrimaryKey(t => t.SesionesId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingsId = c.Int(nullable: false, identity: true),
                        RutaImagenes = c.String(),
                        RutaVideos = c.String(),
                    })
                .PrimaryKey(t => t.SettingsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sesiones", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Saluds", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RegistroEjercicios", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RegistroEjercicios", "EjercicioId", "dbo.Ejercicios");
            DropForeignKey("dbo.Recetas", "TipoRecetaId", "dbo.TipoRecetas");
            DropForeignKey("dbo.Recetas", "TiempoComidaId", "dbo.TiempoComidas");
            DropForeignKey("dbo.Notificacions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ejercicios", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.Sesiones", new[] { "UserId" });
            DropIndex("dbo.Saluds", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RegistroEjercicios", new[] { "EjercicioId" });
            DropIndex("dbo.RegistroEjercicios", new[] { "UserId" });
            DropIndex("dbo.Recetas", new[] { "TiempoComidaId" });
            DropIndex("dbo.Recetas", new[] { "TipoRecetaId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Notificacions", new[] { "UserId" });
            DropIndex("dbo.Ejercicios", new[] { "CategoriaId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropTable("dbo.Settings");
            DropTable("dbo.Sesiones");
            DropTable("dbo.Saluds");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RegistroEjercicios");
            DropTable("dbo.TipoRecetas");
            DropTable("dbo.TiempoComidas");
            DropTable("dbo.Recetas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Notificacions");
            DropTable("dbo.IMCs");
            DropTable("dbo.Ejercicios");
            DropTable("dbo.Categorias");
            DropTable("dbo.AspNetUserRoles");
        }
    }
}
