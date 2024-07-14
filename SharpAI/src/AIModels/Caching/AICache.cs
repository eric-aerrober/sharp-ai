
public class AICache <IKeyType, IValueType> 
{

    private string _cachePath = "./cache";
    private string _fileExtension = ".json";

    public void ConfigureCachePath(string cachePath, string fileExtension = ".json") {
        _cachePath = "./cache/" + cachePath + "/";
        _fileExtension = fileExtension;
        Directory.CreateDirectory(Path.GetDirectoryName(_cachePath));
    }

    protected string valueForKey(IKeyType key)
    {
        return StringUtilities.GetStableHashCode(key.ToString()) + _fileExtension;
    }

    protected string pathForKey(IKeyType key)
    {
        return Path.Combine(_cachePath, valueForKey(key));
    }

    public virtual bool Has (IKeyType key)
    {
        return File.Exists(pathForKey(key));
    }

    protected string GetRaw (IKeyType key)
    {
        return File.ReadAllText(pathForKey(key));
    }

    public virtual IValueType Get (IKeyType key)
    {
        throw new NotImplementedException();
    }

    protected void SetRaw (IKeyType key, string value)
    {
        File.WriteAllText(pathForKey(key), value);
    }

    public virtual void Set (IKeyType key, IValueType value)
    {
        throw new NotImplementedException();
    }

}