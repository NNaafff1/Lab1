using CourseManager.Managers; // Для Singleton
using CourseManager.Models;   // Для моделей Course и Teacher
using Xunit;                  // Для тестирования

public class CourseManagerTests
{
    [Fact]
    public void AddCourse_ShouldIncreaseCourseCount()
    {
        var manager = CourseManagerSingleton.Instance; // Доступ к Singleton
        int initialCount = manager.Courses.Count;
        manager.Courses.Add(new Course("Тестовый курс", CourseType.Online, "Преподаватель"));
        Assert.Equal(initialCount + 1, manager.Courses.Count); // Проверка увеличения
    }

    [Fact]
    public void RemoveCourse_ShouldDecreaseCourseCount()
    {
        var manager = CourseManagerSingleton.Instance; // Доступ к Singleton
        var course = new Course("Тестовый курс", CourseType.Online, "Преподаватель");
        manager.Courses.Add(course);
        int initialCount = manager.Courses.Count;
        manager.Courses.Remove(course);
        Assert.Equal(initialCount - 1, manager.Courses.Count); // Проверка уменьшения
    }
}
