namespace Browsio.Domain
{
    #region << Using >>

    using Incoding.CQRS;

    #endregion

    public class AddGenreCommand : CommandBase
    {
        #region Properties

        public string Name { get; set; }

        public CategoryOfType Category { get; set; }

        #endregion

        public override void Execute()
        {
            this.Repository.Save(new Genre
                                {
                                        Name = this.Name, 
                                        Category = this.Category
                                });
        }
    }
}