using System.Collections.Generic;

public class Path
{
    public string kind { get; set; }
    public string collection { get; set; }
    public string key { get; set; }
    public string @ref { get; set; }
    public object reftime { get; set; }
}

public class Value
{
    public string id { get; set; }
    public string name { get; set; }
}

public class NewValue
{
    public string name { get; set; }
}

public class Result
{
    public Path path { get; set; }
    public Value value { get; set; }
    public object reftime { get; set; }
}

public class RootObject
{
    public int count { get; set; }
    public List<Result> results { get; set; }
}
