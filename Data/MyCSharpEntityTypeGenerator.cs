using Data.CodeTemplates;
using EntityFrameworkCore.Scaffolding.Handlebars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace Data;

public class MyCSharpEntityTypeGenerator : HbsCSharpEntityTypeGenerator
{
    private readonly IOptions<HandlebarsScaffoldingOptions> _options;
    private readonly List<CustomProperty> _customProperties;
    
    public MyCSharpEntityTypeGenerator(IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper, IEntityTypeTemplateService entityTypeTemplateService, IEntityTypeTransformationService entityTypeTransformationService, IOptions<HandlebarsScaffoldingOptions> options) : base(annotationCodeGenerator, cSharpHelper, entityTypeTemplateService, entityTypeTransformationService, options)
    {
        _options = options;
        _customProperties = new List<CustomProperty>();
    }

    protected override void GenerateProperties(IEntityType entityType)
    {
        if (entityType == null) throw new ArgumentNullException(nameof(entityType));

        var properties = new List<Dictionary<string, object>>();

        foreach (var property in entityType.GetProperties())
        {
            PropertyAnnotationsData = new List<Dictionary<string, object>>();

            var propertyType = CSharpHelper.Reference(property.ClrType);
            if (property.IsNullable && !propertyType.EndsWith("?"))
            {
                propertyType += "?";
            }

            var customProperty = _customProperties.FirstOrDefault(x => x.TableName == property.DeclaringEntityType.Name
                                                                       && x.PropertyName == property.Name);
            var isCustomProperty = customProperty != null;
            

            if (UseDataAnnotations)
            {
                GeneratePropertyDataAnnotations(property);
            }

            properties.Add(new Dictionary<string, object>
            {
                { "property-type", propertyType },
                { "property-name", property.Name },
                { "property-annotations",  PropertyAnnotationsData },
                { "property-comment", property.GetComment() },
                { "property-isnullable", property.IsNullable },
                { "nullable-reference-types", property.IsNullable },
                
                // Add new item to template data
                { "is-custom-property", isCustomProperty},
                { "custom-property", (customProperty != null) ? customProperty.Property : "" },
                { "property-entity-name", $"{property.DeclaringEntityType.Name}->{property.Name}" }
            });
        }

        var transformedProperties = EntityTypeTransformationService.TransformProperties(entityType, properties);
        
        // Add to transformed properties
        for (int i = 0; i < transformedProperties.Count ; i++)
        {
            transformedProperties[i].Add("is-custom-property", properties[i]["is-custom-property"]);
            transformedProperties[i].Add("custom-property", properties[i]["custom-property"]);
            transformedProperties[i].Add("property-entity-name", properties[i]["property-entity-name"]);
        }

        TemplateData.Add("properties", transformedProperties);
    }
}