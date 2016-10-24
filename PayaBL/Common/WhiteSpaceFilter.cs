using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PayaBL.Common
{
    public class WhiteSpaceFilter : Stream
{
    // Fields
    private readonly Stream _sink;
    private static readonly Regex Reg = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}");

    // Methods
    public WhiteSpaceFilter(Stream sink)
    {
        _sink = sink;
    }

    public override void Close()
    {
        _sink.Close();
    }

    public override void Flush()
    {
        _sink.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return _sink.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _sink.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _sink.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        var data = new byte[count];
        Buffer.BlockCopy(buffer, offset, data, 0, count);
        string html = Encoding.Default.GetString(buffer);
        html = Reg.Replace(html, string.Empty);
        byte[] outdata = Encoding.Default.GetBytes(html);
        _sink.Write(outdata, 0, outdata.GetLength(0));
    }

    // Properties
    public override bool CanRead
    {
        get
        {
            return true;
        }
    }

    public override bool CanSeek
    {
        get
        {
            return true;
        }
    }

    public override bool CanWrite
    {
        get
        {
            return true;
        }
    }

    public override long Length
    {
        get
        {
            return 0L;
        }
    }

    public override long Position { get; set; }
}

 

 

}
