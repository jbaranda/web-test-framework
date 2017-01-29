using OpenQA.Selenium;

namespace Framework.Elements
{
    public class Image : PageLink
    {
        public string Alt => Element.GetAlt();
        public string Src => Element.GetSrc();

        public Image(ISearchContext context, By selector) : base(context, selector) { }

        public override string ToString()
        {
            return $"{base.ToString()},Alt={Alt},Src={Src}";
        }
    }
}
