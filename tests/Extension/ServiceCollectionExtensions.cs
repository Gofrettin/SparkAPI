using Microsoft.Extensions.DependencyInjection;
using Moq;
using Spark.Core.Enum;
using Spark.Database;
using Spark.Database.Data;
using Spark.Extension;
using Spark.Processor;

namespace Spark.Tests.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            var dbMock = new Mock<IDatabase>();

            dbMock.Setup(x => x.Monsters.GetValue(It.IsAny<int>())).Returns(new MonsterData());
            dbMock.Setup(x => x.Maps.GetValue(It.IsAny<int>())).Returns(new MapData
            {
                Grid = new byte[999]
            });
            dbMock.Setup(x => x.Skills.GetValue(It.IsAny<int>())).Returns(new SkillData
            {
                Category = SkillCategory.Player
            });

            services.AddSingleton(dbMock.Object);
            
            services.AddGameFactories();

            services.AddImplementingTypes<IPacketProcessor>();
        }
    }
}