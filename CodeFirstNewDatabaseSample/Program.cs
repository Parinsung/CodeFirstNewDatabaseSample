// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

Console.WriteLine("Hello, World!");
using (var db = new BloggingContext())
{
    // Create and save a new Blog
    Console.Write("Enter a name for a new Blog: ");
    var name = Console.ReadLine();

    var blog = new Blog { Name = name };
    db.Blogs.Add(blog);
    db.SaveChanges();

    // Display all Blogs from the database
    var query = from b in db.Blogs
                orderby b.Name
                select b;

    Console.WriteLine("All blogs in the database:");
    foreach (var item in query)
    {
        Console.WriteLine(item.Name);
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}


public class Blog
{
    public int BlogId { get; set; }
    public string Name { get; set; }

    //public DateTime CreatedDate { get; set; }

    public virtual List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}

public class BloggingContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");
        }
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Blog>()
    //        .Property(b => b.CreatedDate)
    //        .HasDefaultValueSql("getdate()");
    //}
}



//Add-Migration InitialCreate - Context YourDbContextName
//Update-Database
//Remove-Migration 
//Add-Migration MyMigration -Context YourDbContextName
//Add-Migration MyMigration -Context BloggingContext