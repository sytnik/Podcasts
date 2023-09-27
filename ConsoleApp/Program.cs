var tclass = new tClass("dev", 1);
tclass.Id = 1;
var tclass2 = new tClass2(1, "dev");
var tclass3 = new tClass3("dev", 1);
var tclass4 = new tClass4(1, "dev");
var updclass = tclass4 with {Name = "dev2"};

var hello = "Hello World!";

public sealed class tClass(string name, int id)
{
    public int Id { get; set; } = id;
    public string Name = name;
}

public class tClass2(int Id, string Name);

public sealed record tClass3(string Name, int Id)
{
    public int Id { get; set; } = Id;
    public string Name = Name;
}

public record tClass4(int Id, string Name);

public readonly record struct tStruct(int id, string name)
{
    public int Id { get; init; } = id;
    public readonly string Name = name;
}

public readonly record struct
    tStruct1(int Id, string Name);