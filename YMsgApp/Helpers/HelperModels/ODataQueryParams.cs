using System.Text;

namespace YMsgApp.Helpers.HelperModels;

public class ODataQueryParams
{
    private int? Top { get; set; }

    private int? Skip { get; set; }

    private bool Count { get; set; } = false;

    private string[]? Select { get; set; }

    private string[]? Expand { get; set; }

    private string[]? Filter { get; set; }

    public ODataQueryParams()
    {
        
    }

    public ODataQueryParams AddCount()
    {
        if(Count) throw new InvalidOperationException("Count parameter already set");
        Count = true;
        return this;
    }

    public ODataQueryParams AddTop(int value)
    {
        if (Top != null) throw new InvalidOperationException("Top parameter already set");
            Top = value;
            return this;
    }
    
    public ODataQueryParams AddSkip(int value)
    {
        if (Skip != null) throw new InvalidOperationException("Skip parameter already set");
        Skip = value;
        return this;
    }
    
    public ODataQueryParams AddSelect(params string[] value)
    {
        if (Select != null) throw new InvalidOperationException("Select parameter already set");
        Select = value;
        return this;
    }
    
    public ODataQueryParams AddExpand(params string[] value)
    {
        if (Expand != null) throw new InvalidOperationException("Expand parameter already set");
        Expand = value;
        return this;
    }
    
    public ODataQueryParams AddFilter(params string[] value)
    {
        if (Filter != null) throw new InvalidOperationException("Filter parameter already set");
        Filter = value;
        return this;
    }

    public (string,string)[] Compile()
    {
        var res=new List<(string, string)>();
        if (Top == null && Skip == null && Select == null && Expand == null && Filter == null)
        {
            return res.ToArray();
        }

        if (Top != null)
        {
            res.Add(("$top",Top.ToString()));
        }
        
        if (Skip != null)
        {
            res.Add(("$skip",Skip.ToString()));
        }
        
        if (Select != null)
        {
            res.Add(("$select",string.Join(",",Select)));
        }
        
        if (Expand != null)
        {
            res.Add(("$expand",string.Join(",",Expand)));
        }
        
        if (Filter != null)
        {
            res.Add(("$filter",string.Join(",",Filter)));
        }
        
        if (Count)
        {
            res.Add(("$count","true"));
        }

        return res.ToArray();
    }
}