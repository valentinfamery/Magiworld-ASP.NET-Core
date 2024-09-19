using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class GamePartContext : DbContext
{
    public DbSet<GamePart> GameParts { get; set; }
    

    public string DbPath { get; }

    public GamePartContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "gameparts.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class GamePart{

  public int GamePartId { get; set; }
  public string? playerVictorious { get; set; }
}


