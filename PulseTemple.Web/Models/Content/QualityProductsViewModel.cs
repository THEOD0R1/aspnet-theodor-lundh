namespace PulseTemple.Web.Models.Content;

public sealed record QualityProductsViewModel(
    string SmallTitle,
    string Title,
    IReadOnlyList<QualityProductsImg> ImgContentList,
    string SmallText
    );
