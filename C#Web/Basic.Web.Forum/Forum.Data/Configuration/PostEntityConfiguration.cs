﻿namespace Forum.Data.Configuration;

using Forum.Data.Models;
using Forum.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    private readonly PostSeeder postSeeder;

    public PostEntityConfiguration()
    {
        this.postSeeder = new PostSeeder(); 
    }

    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasData(this.postSeeder.GeneratePost().ToList());
    }
}
