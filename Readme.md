


https://github.com/user-attachments/assets/22e33a13-0245-46c0-81c2-3d892621a8c8


# what you get if research
- comfortable ui/ux for everyday witch must be at all messendgers
- 'must have' backend systems like telegram (in progress)
# state
- .. progress
# what for
- pet proj which out of bounds
# devops
- .. progress
# deploy
- if u have PROBLEM WITH VISUAL STUDIO SPA ASP.NET VUE.JS ENTITYFRAMEWORK MIGRATIONS VUE-ROUTE WEB-SOCKET HTTP-REQUEST see how to deploy step by step
    
solution > entityframeworkcore > mark for server > install  
solution > entityframeworkcore.sql > mark for server > install  
solution > entityframeworkcore.design > mark for server > install  
solution > entity > mark for server > install  
solution > swagger> mark for server > install  
controllers > add > use entityframework > select model > create new context > selsect sql  
programm.cs > replace > builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v2", new OpenApiInfo { Title = "MVCCallWebAPI", Version = "v2" });});  
programm.cs > replace > app.UseSwaggerUI(c => {  c.SwaggerEndpoint("/swagger/v2/swagger.json", "MVCCallWebAPI"); });  
comment other method  
DBcontext > add like this > protected override void OnModelCreating(ModelBuilder modelBuilder) {modelBuilder.Entity<Chat>().ToTable("Chat");}  
DBcontext > add like this > Database.EnsureCreated();  
optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=steadyforumServerContext-53affff5-4b9a-4b21-bdc1-c3d17508cc0d;Trusted_Connection=True;MultipleActiveResultSets=true;");  
cmd > dotnet ef migration add init  
cahnge > proxy: { '^/api': {
