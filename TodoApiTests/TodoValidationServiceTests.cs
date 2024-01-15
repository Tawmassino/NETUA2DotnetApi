using NETUA2DotnetApi.Dtos;
using NETUA2DotnetApi.Services;

namespace TodoApiTests
{
    public class TodoValidationServiceTests
    {
        [Fact]
        public void IsValid_WhenTypeIsWork()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Work", // <- cia yra testuojama reiksme
                Content = "test",
                EndDate = DateTime.Now.AddDays(1)
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.True(actual);
        }
        [Fact]
        public void IsValid_WhenTypeIsHome()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Home", // <- cia yra testuojama reiksme
                Content = "test",
                EndDate = DateTime.Now.AddDays(1)
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.True(actual);
        }

        [Fact]
        public void IsValid_WhenTypeIsHobby()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Hobby", // <- cia yra testuojama reiksme
                Content = "test",
                EndDate = DateTime.Now.AddDays(1)
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.True(actual);
        }

        [Fact]
        public void IsValid_WhenTypeIsOther()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Other", // <- cia yra testuojama reiksme
                Content = "test",
                EndDate = DateTime.Now.AddDays(1)
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.False(actual);
        }

        [Fact]
        public void IsValid_WhenDateIsToday()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Hobby",
                Content = "test",
                EndDate = DateTime.Now // <- cia yra testuojama reiksme
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.True(actual);
        }

        [Fact]
        public void IsValid_WhenDateIsYesturday()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Hobby",
                Content = "test",
                EndDate = DateTime.Now.AddDays(-1) // <- cia yra testuojama reiksme
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.False(actual);
        }

        [Fact]
        public void IsValid_WhenDateIs2Months()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Hobby",
                Content = "test",
                EndDate = DateTime.Now.AddMonths(2) // <- cia yra testuojama reiksme
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.False(actual);
        }


        [Fact]
        public void IsValid_WhenDateIsNull()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Hobby",
                Content = "test",
                EndDate = null // <- cia yra testuojama reiksme
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.True(actual);
        }

        [Fact]
        public void IsValid_WhenContentIsMoreThen200CharLenght()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Hobby",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec euismod, nisl quis aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl quis aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl quis aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl quis aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl quis aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl quis aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl quis aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nisl eu nisl. Sed euismod, nisl quis aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nisl eu nisl.", // <- cia yra testuojama reiksme
                EndDate = DateTime.Now.AddDays(1)
            };
            //act
            var sut = new TodoValidationService(); //sut - service under test
            var actual = sut.IsValid(fake);
            //assert
            Assert.False(actual);

        }

    }
}