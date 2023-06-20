using Microsoft.EntityFrameworkCore;
using SongsAndPlaylist.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SongsAndPlaylist.Data
{
    public class ApplicationDbContext : DbContext /*ApplicationDbContext :Entity Framework Core에서 사용되는 DbContext 클래스를 상속하여 만들어진 데이터베이스 컨텍스트 클래스, DbContext 클래스는 Entity Framework에서 데이터베이스와 애플리케이션 코드 간의 중개자 역할을 합니다. 이 클래스를 사용하여 Entity Framework의 다양한 기능을 사용할 수 있으며, 데이터베이스와 데이터 모델 간의 매핑을 설정하고, 데이터베이스와의 CRUD(Create, Read, Update, Delete) 작업을 수행*/
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Playlists> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaylistSong>()
               .HasKey(ps => new { ps.PlaylistId, ps.SongId });

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSongs)
                .HasForeignKey(ps => ps.PlaylistId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistSongs)
                .HasForeignKey(ps => ps.SongId);
        }
    }
}
