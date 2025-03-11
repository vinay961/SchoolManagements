## What new things we learn while developing this project?
- **How to handle images in ASP.NET MVC**
- **How to manage sessions in ASP.NET MVC**
- **How to implement search functionality in ASP.NET MVC**
- **How to make connections between database tables in ASP.NET MVC**

1. **How to handle images in ASP.NET MVC**
- To handle images we first create a variable env for the hosting environment. Then we create a folder named "images" in the wwwroot folder. We save the images in this folder. We use the following code to save the image in the folder.
```csharp
    string fileName = "";
    if(s.ImageFile != null)
    {
        string folder = Path.Combine(env.WebRootPath, "images");
        fileName = Guid.NewGuid().ToString() + "_" + s.ImageFile.FileName;
        string filePath = Path.Combine(folder, fileName);
        s.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create)); 

        Students student = new Students
        {
            Name = s.Name,
            FatherName = s.FatherName,
            Email = s.Email,
            Standard = s.Standard,
            ClassId = classEntity.Id,
            ProfileImage = fileName
        };
        dbContext.Students.Add(student);
        await dbContext.SaveChangesAsync();
        return RedirectToAction("SuccessPage");
    }
```

2. **How to manage sessions in ASP.NET MVC**
- To manage or use session we first need to update the program file.
```csharp
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30); 
        options.Cookie.HttpOnly = true; // Prevents client-side access to cookies
        options.Cookie.IsEssential = true; // Ensures session works even if tracking is disabled
    });
```
Then we can use the session in the controller. We use the following code to set the session.
```csharp
    HttpContext.Session.SetString("SessionKeyName", "SessionValue");
```

3. **How to make connections between tables in ASP.NET MVC**
- We need to declare the foreign key in the model class. We use the following code to declare the foreign key.
```csharp
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Standard { get; set; }
        public string ProfileImage { get; set; }
        public int ClassId { get; set; }
        public Classes Class { get; set; }
    }
```
Then we use the following code to make the connection between tables.
```csharp
    public class Classes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Students> Students { get; set; }
    }
```