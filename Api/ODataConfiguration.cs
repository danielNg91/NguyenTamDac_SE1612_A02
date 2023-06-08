using BusinessObjects;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Api;

public static class ODataConfiguration
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
        builder.EntitySet<Employee>("Employees");
        builder.EntitySet<CompanyProject>("CompanyProjects");
        builder.EntitySet<Department>("Departments");
        builder.EntitySet<ParticipatingProject>("ParticipatingProjects");
        return builder.GetEdmModel();
    }
}
