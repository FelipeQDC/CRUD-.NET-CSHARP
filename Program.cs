var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var listaAlunos = new List<Aluno>();

app.MapGet("/", () => "Hello World!");
app.MapGet("/santa", () => "Universidade Santa Cecilia");
app.MapGet("/santa/{nome}", (string nome) => {
   return Results.Ok($"Valor: {nome}");
});
//Post
app.MapPost("/Aluno", (Aluno UmAluno) => {
    listaAlunos.Add(UmAluno);
return Results.Created($"/aluno/{UmAluno.Id}",UmAluno);
});

//Get
app.MapGet("/Aluno", () => {
 
   return Results.Ok(listaAlunos.ToList());
});

//Get
app.MapGet("/Aluno/{id}", (int id) => {
    var aluno = listaAlunos.FirstOrDefault(i => i.Id == id);
    if(aluno == null) return Results.NotFound();

    return Results.Ok(aluno);
});

//Delete
app.MapDelete("/Aluno/{id}", (int id) => {
var aluno = listaAlunos.FirstOrDefault(a => a.Id == id);

if(aluno is null) 
{
    return Results.NotFound();
}

listaAlunos.Remove(aluno);
return Results.Ok(aluno);
});

//Put - Update
app.MapPut("/aluno/{id}", (int id, Aluno umAluno) => {

var aluno = listaAlunos.FirstOrDefault(a => a.Id == id);

if(aluno is null) 
{
    return Results.NotFound();
}

aluno.Nome = umAluno.Nome;
aluno.DataDeNasc = umAluno.DataDeNasc;
return Results.Ok(aluno);
});

app.Run();

public class Aluno
{
    public int Id { get; set;}
    public string Nome { get; set;}
    public string DataDeNasc { get; set;} 
}
 