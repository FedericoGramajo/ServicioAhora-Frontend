using System.Collections.Generic;

namespace BlazorWasm.Shared.Landing;

public record NavLinkModel(string Text, string Href);
public record MenuActionModel(string Text, string Href);
public record SearchConfig(string Placeholder, string ButtonText);
public record HeroCtaModel(string Text, string CssClass);
public record HeroMetricModel(string Value, string Caption);
public record ServiceModel(string Slug, string Name, string Description, string Category, string ImageUrl, decimal Price);
public record ProfessionalModel(string Slug, string Name, string Specialty, string City, string ImageUrl, double Rating, int Projects);
public record TrustStatModel(string Value, string Caption);
public record FooterLinkModel(string Text, string Href);
public record FooterGroupModel(string Title, IEnumerable<FooterLinkModel> Links);
