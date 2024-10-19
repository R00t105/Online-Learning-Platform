using Microsoft.AspNetCore.Identity;
using OnlineLearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Data
{
    public static class SeedData
    {
        public static async Task Initialize(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Tracks.Count() < 1)
            {
                var tracksToAdd = new List<Track>
            {
                new Track { Name = "Web Development", Description = "Learn the fundamentals of web development, including HTML, CSS, and JavaScript." },
                new Track { Name = "Data Science", Description = "Explore the world of data science, including Python, machine learning, and data analysis." },
                new Track { Name = "Mobile Development", Description = "Create mobile apps with Android and iOS platforms using modern development tools." },
                new Track { Name = "Cybersecurity", Description = "Understand the basics of network security, cryptography, and risk management." },
                new Track { Name = "Full-Stack Web Development", Description = "Master frontend and backend development", CreationDate = new DateTime(2023, 01, 15) },
                new Track { Name = "Data Science with Python", Description = "Leverage machine learning and data analysis", CreationDate = new DateTime(2023, 02, 20) },
                new Track { Name = "Cybersecurity Fundamentals", Description = "Learn network and system security principles", CreationDate = new DateTime(2023, 03, 10) },
                new Track { Name = "Cloud Architecture (AWS)", Description = "Design and implement cloud-based solutions", CreationDate = new DateTime(2023, 04, 05) },
                new Track { Name = "DevOps and CI/CD", Description = "Automate software development and deployment", CreationDate = new DateTime(2023, 05, 15) },
                new Track { Name = "Artificial Intelligence", Description = "Explore machine learning and deep learning algorithms", CreationDate = new DateTime(2023, 06, 20) },
                new Track { Name = "Mobile App Development (iOS)", Description = "Build native iOS applications", CreationDate = new DateTime(2023, 07, 10) },
                new Track { Name = "Blockchain Technology", Description = "Understand blockchain concepts and applications", CreationDate = new DateTime(2023, 08, 05) },
                new Track { Name = "UI/UX Design", Description = "Create user-centered designs", CreationDate = new DateTime(2023, 09, 15) },
                new Track { Name = "Software Testing and Quality Assurance", Description = "Ensure software quality", CreationDate = new DateTime(2023, 10, 20) },
                new Track { Name = "Advanced Database Systems", Description = "Master database design and management", CreationDate = new DateTime(2023, 11, 10) },
                new Track { Name = "Ethical Hacking", Description = "Learn ethical hacking techniques", CreationDate = new DateTime(2023, 12, 05) },
                new Track { Name = "Big Data Analytics", Description = "Analyze large datasets", CreationDate = new DateTime(2024, 01, 15) },
                new Track { Name = "Internet of Things (IoT)", Description = "Develop IoT applications", CreationDate = new DateTime(2024, 02, 20) },
                new Track { Name = "Game Development", Description = "Create interactive games", CreationDate = new DateTime(2024, 03, 10) },
                new Track { Name = "Data Visualization", Description = "Communicate data effectively", CreationDate = new DateTime(2024, 04, 05) },
                new Track { Name = "Low-Code/No-Code Development", Description = "Build applications without extensive coding", CreationDate = new DateTime(2024, 05, 15) },
                new Track { Name = "Cybersecurity Incident Response", Description = "Handle security breaches", CreationDate = new DateTime(2024, 06, 20) },
                new Track { Name = "Machine Learning Operations (MLOps)", Description = "Deploy and manage ML models", CreationDate = new DateTime(2024, 07, 10) },
                new Track { Name = "Quantum Computing Fundamentals", Description = "Explore quantum computing concepts", CreationDate = new DateTime(2024, 08, 05) }
            };
                context.Tracks.AddRange(tracksToAdd);
                await context.SaveChangesAsync();
            }

            if (context.Courses.Count() < 1)
            {
                var coursesToAdd = new List<Course>
            {
                new Course { Title = "HTML & CSS Basics", Description = "Start with the basics of HTML and CSS to build your first website.", TrackId = context.Tracks.First(t => t.Name == "Web Development").Id },
                new Course { Title = "JavaScript for Beginners", Description = "Learn the basics of JavaScript to make your websites interactive.", TrackId = context.Tracks.First(t => t.Name == "Web Development").Id },
                new Course { Title = "Introduction to Data Science", Description = "Understand the fundamental concepts of data science and machine learning.", TrackId = context.Tracks.First(t => t.Name == "Data Science").Id },
                new Course { Title = "Python for Data Analysis", Description = "Learn to use Python for data analysis, manipulation, and visualization.", TrackId = context.Tracks.First(t => t.Name == "Data Science").Id },
                new Course { Title = "Advanced Database Systems", Description = "Master database design and management", CreationDate = new DateTime(2024, 01, 15), TrackId = 11 },
                new Course { Title = "Data Visualization with Tableau", Description = "Create compelling data visualizations", CreationDate = new DateTime(2024, 02, 20), TrackId = 14 },
                new Course { Title = "Ethical Hacking Techniques", Description = "Learn ethical hacking principles and methods", CreationDate = new DateTime(2024, 03, 10), TrackId = 12 },
                new Course { Title = "Cloud Security Fundamentals", Description = "Protect cloud-based systems", CreationDate = new DateTime(2024, 04, 05), TrackId = 4 },
                new Course { Title = "Agile Project Management", Description = "Implement agile methodologies", CreationDate = new DateTime(2024, 05, 15), TrackId = 15 },
                new Course { Title = "Natural Language Processing (NLP)", Description = "Work with textual data", CreationDate = new DateTime(2024, 06, 20), TrackId = 6 },
                new Course { Title = "Mobile App Development (Android)", Description = "Build native Android applications", CreationDate = new DateTime(2024, 07, 10), TrackId = 7 },
                new Course { Title = "Blockchain Development", Description = "Create blockchain-based solutions", CreationDate = new DateTime(2024, 08, 05), TrackId = 8 },
                new Course { Title = "User Experience (UX) Design", Description = "Design user-friendly interfaces", CreationDate = new DateTime(2024, 09, 15), TrackId = 9 },
                new Course { Title = "Software Testing Automation", Description = "Automate testing processes", CreationDate = new DateTime(2024, 10, 20), TrackId = 10 },
                new Course { Title = "Data Mining and Analysis", Description = "Discover patterns in data", CreationDate = new DateTime(2024, 11, 10), TrackId = 2 },
                new Course { Title = "Cybersecurity Incident Response Planning", Description = "Prepare for security incidents", CreationDate = new DateTime(2024, 12, 05), TrackId = 12 },
                new Course { Title = "Machine Learning Algorithms", Description = "Understand ML algorithms", CreationDate = new DateTime(2025, 01, 15), TrackId = 6 },
                new Course { Title = "IoT Device Development", Description = "Create IoT devices", CreationDate = new DateTime(2025, 02, 20), TrackId = 14 },
                new Course { Title = "Game Design and Development", Description = "Create interactive games", CreationDate = new DateTime(2025, 03, 10), TrackId = 15 },
                new Course { Title = "Data Visualization with Python", Description = "Create data visualizations using Python", CreationDate = new DateTime(2025, 04, 05), TrackId = 14 },
                new Course { Title = "Low-Code/No-Code Development Platforms", Description = "Explore low-code/no-code tools", CreationDate = new DateTime(2025, 05, 15), TrackId = 15 },
                new Course { Title = "Cybersecurity Threat Intelligence", Description = "Understand emerging threats", CreationDate = new DateTime(2025, 06, 20), TrackId = 12 },
                new Course { Title = "MLOps Tools and Practices", Description = "Implement MLOps workflows", CreationDate = new DateTime(2025, 07, 10), TrackId = 6 },
                new Course { Title = "Quantum Computing Applications", Description = "Explore quantum computing use cases", CreationDate = new DateTime(2025, 08, 05), TrackId = 16 },
                new Course { Title = "AI Ethics and Bias", Description = "Understand ethical considerations in AI", CreationDate = new DateTime(2025, 09, 15), TrackId = 6 },
                new Course { Title = "DevOps Automation with Ansible", Description = "Automate DevOps tasks with Ansible", CreationDate = new DateTime(2025, 10, 20), TrackId = 5 },
                new Course { Title = "Data Engineering with Apache Spark", Description = "Process and analyze large datasets", CreationDate = new DateTime(2025, 11, 10), TrackId = 2 },
                new Course { Title = "Network Security Essentials", Description = "Secure network infrastructure", CreationDate = new DateTime(2025, 12, 05), TrackId = 4 },
                new Course { Title = "Deep Learning with TensorFlow", Description = "Build deep learning models", CreationDate = new DateTime(2026, 01, 15), TrackId = 6 },
                new Course { Title = "IoT Security and Privacy", Description = "Protect IoT devices", CreationDate = new DateTime(2026, 02, 20), TrackId = 14 },
                new Course { Title = "Game Development with Unity", Description = "Create games using Unity", CreationDate = new DateTime(2026, 03, 10), TrackId = 15 },
                new Course { Title = "Data Visualization with D3.js", Description = "Create interactive visualizations", CreationDate = new DateTime(2026, 04, 05), TrackId = 14 },
                new Course { Title = "Serverless Computing with AWS Lambda", Description = "Build serverless applications", CreationDate = new DateTime(2026, 05, 15), TrackId = 4 },
                new Course { Title = "Cybersecurity Governance and Risk Management", Description = "Manage cybersecurity risks", CreationDate = new DateTime(2026, 06, 20), TrackId = 12 },
                new Course { Title = "MLOps with Kubernetes", Description = "Deploy ML models on Kubernetes", CreationDate = new DateTime(2026, 07, 10), TrackId = 6 },
                new Course { Title = "Quantum Machine Learning", Description = "Explore quantum machine learning algorithms", CreationDate = new DateTime(2026, 08, 05), TrackId = 16 },
                new Course { Title = "AI Explainability and Interpretability", Description = "Understand AI decision-making", CreationDate = new DateTime(2026, 09, 15), TrackId = 6 },
                new Course { Title = "DevOps with Terraform", Description = "Manage infrastructure with Terraform", CreationDate = new DateTime(2026, 10, 20), TrackId = 5 },
                new Course { Title = "Real-time Data Processing with Apache Kafka", Description = "Process real-time data streams", CreationDate = new DateTime(2026, 11, 10), TrackId = 2 },
                new Course { Title = "Cloud Native Security", Description = "Secure cloud-native applications", CreationDate = new DateTime(2026, 12, 05), TrackId = 4 },
                new Course { Title = "Generative Adversarial Networks (GANs)", Description = "Create generative models", CreationDate = new DateTime(2027, 01, 15), TrackId = 6 },
                new Course { Title = "IoT Edge Computing", Description = "Process data at the edge", CreationDate = new DateTime(2027, 02, 20), TrackId = 14 },
                new Course { Title = "Game Development with Unreal Engine", Description = "Create games using Unreal Engine", CreationDate = new DateTime(2027, 03, 10), TrackId = 15 }
            };
                context.Courses.AddRange(coursesToAdd);
                await context.SaveChangesAsync();
            }



            if (context.Contents.Count() < 1)
            {
                var contentsToAdd = new List<Content>
                {
                    new Content { Title = "Introduction to HTML", CourseId = 1, IsRead = false },
                    new Content { Title = "CSS Styling", CourseId = 1, IsRead = false },
                    new Content { Title = "JavaScript Variables", CourseId = 2, IsRead = false },
                    new Content { Title = "Python for Data Science", CourseId = 3, IsRead = false },
                    new Content { Title = "Cybersecurity Fundamentals", CourseId = 4, IsRead = false },
                    new Content { Title = "Cloud Architecture (AWS)", CourseId = 5, IsRead = false },
                    new Content { Title = "DevOps and CI/CD", CourseId = 6, IsRead = false },
                    new Content { Title = "Artificial Intelligence", CourseId = 7, IsRead = false },
                    new Content { Title = "Mobile App Development (iOS)", CourseId = 8, IsRead = false },
                    new Content { Title = "Blockchain Technology", CourseId = 9, IsRead = false },
                    new Content { Title = "UI/UX Design", CourseId = 10, IsRead = false },
                    new Content { Title = "Software Testing and Quality Assurance", CourseId = 11, IsRead = false },
                    new Content { Title = "Advanced Database Systems", CourseId = 12, IsRead = false },
                    new Content { Title = "Ethical Hacking", CourseId = 13, IsRead = false },
                    new Content { Title = "Big Data Analytics", CourseId = 14, IsRead = false },
                    new Content { Title = "Internet of Things (IoT)", CourseId = 15, IsRead = false },
                    new Content { Title = "Game Development", CourseId = 16, IsRead = false },
                    new Content { Title = "Data Visualization", CourseId = 17, IsRead = false },
                    new Content { Title = "Low-Code/No-Code Development", CourseId = 18, IsRead = false },
                    new Content { Title = "Cybersecurity Incident Response", CourseId = 19, IsRead = false },
                    new Content { Title = "Machine Learning Operations (MLOps)", CourseId = 20, IsRead = false },
                    new Content { Title = "Quantum Computing Fundamentals", CourseId = 21, IsRead = false },
                    new Content { Title = "HTML5 Semantic Elements", CourseId = 1, IsRead = false },
                    new Content { Title = "CSS Media Queries", CourseId = 1, IsRead = false },
                    new Content { Title = "JavaScript DOM Manipulation", CourseId = 2, IsRead = false },
                    new Content { Title = "Python Libraries for Data Science", CourseId = 3, IsRead = false },
                    new Content { Title = "Network Security Principles", CourseId = 4, IsRead = false },
                    new Content { Title = "AWS EC2 and S3", CourseId = 5, IsRead = false },
                    new Content { Title = "Git and Version Control", CourseId = 6, IsRead = false },
                    new Content { Title = "Machine Learning Algorithms", CourseId = 7, IsRead = false },
                    new Content { Title = "iOS App Development Fundamentals", CourseId = 8, IsRead = false },
                    new Content { Title = "Blockchain Consensus Mechanisms", CourseId = 9, IsRead = false },
                    new Content { Title = "User Interface Design Principles", CourseId = 10, IsRead = false },
                    new Content { Title = "Test-Driven Development (TDD)", CourseId = 11, IsRead = false },
                    new Content { Title = "Relational Database Design", CourseId = 12, IsRead = false },
                    new Content { Title = "Ethical Hacking Techniques", CourseId = 13, IsRead = false },
                    new Content { Title = "Big Data ETL Processes", CourseId = 14, IsRead = false },
                    new Content { Title = "IoT Device Prototyping", CourseId = 15, IsRead = false },
                    new Content { Title = "Game Design Principles", CourseId = 16, IsRead = false },
                    new Content { Title = "Data Visualization with Python", CourseId = 17, IsRead = false },
                    new Content { Title = "Low-Code/No-Code Platform Overview", CourseId = 18, IsRead = false },
                    new Content { Title = "Cybersecurity Incident Response Planning", CourseId = 19, IsRead = false },
                    new Content { Title = "MLOps Tools and Practices", CourseId = 20, IsRead = false },
                    new Content { Title = "Quantum Computing Concepts", CourseId = 21, IsRead = false },
                    new Content { Title = "CSS Flexbox Layout", CourseId = 1, IsRead = false },
                    new Content { Title = "JavaScript Events and Event Listeners", CourseId = 2, IsRead = false },
                    new Content { Title = "Data Cleaning and Preprocessing", CourseId = 3, IsRead = false },
                    new Content { Title = "Network Security Best Practices", CourseId = 4, IsRead = false },
                    new Content { Title = "AWS Lambda and API Gateway", CourseId = 5, IsRead = false },
                    new Content { Title = "Continuous Integration and Delivery (CI/CD)", CourseId = 6, IsRead = false },
                    new Content { Title = "Machine Learning Model Deployment", CourseId = 7, IsRead = false },
                    new Content { Title = "iOS App Testing and Debugging", CourseId = 8, IsRead = false },
                    new Content { Title = "Smart Contracts and Solidity", CourseId = 9, IsRead = false },
                    new Content { Title = "User Experience Research Methods", CourseId = 10, IsRead = false },
                    new Content { Title = "Software Testing Automation Tools", CourseId = 11, IsRead = false },
                    new Content { Title = "NoSQL Databases", CourseId = 12, IsRead = false },
                    new Content { Title = "Ethical Hacking Tools and Techniques", CourseId = 13, IsRead = false },
                    new Content { Title = "Big Data Visualization Tools", CourseId = 14, IsRead = false },
                    new Content { Title = "IoT Device Communication Protocols", CourseId = 15, IsRead = false },
                    new Content { Title = "Game Development Tools and Engines", CourseId = 16, IsRead = false },
                    new Content { Title = "Data Visualization with Tableau", CourseId = 17, IsRead = false },
                    new Content { Title = "Low-Code/No-Code Platform Comparison", CourseId = 18, IsRead = false },
                    new Content { Title = "Cybersecurity Incident Response Case Studies", CourseId = 19, IsRead = false },
                    new Content { Title = "MLOps with Kubernetes", CourseId = 20, IsRead = false },
                    new Content { Title = "Quantum Computing Algorithms", CourseId = 21, IsRead = false },
                    new Content { Title = "CSS Grid Layout", CourseId = 1, IsRead = false },
                    new Content { Title = "JavaScript Debugging and Troubleshooting", CourseId = 2, IsRead = false },
                    new Content { Title = "Data Analysis with Pandas", CourseId = 3, IsRead = false },
                    new Content { Title = "Network Security Monitoring and Analysis", CourseId = 4, IsRead = false },
                    new Content { Title = "AWS Serverless Architecture", CourseId = 5, IsRead = false },
                    new Content { Title = "Continuous Delivery Pipelines", CourseId = 6, IsRead = false },
                    new Content { Title = "Machine Learning Model Evaluation", CourseId = 7, IsRead = false },
                    new Content { Title = "iOS App Design and User Experience", CourseId = 8, IsRead = false },
                    new Content { Title = "Blockchain Smart Contract Security", CourseId = 9, IsRead = false },
                    new Content { Title = "User Research and Testing", CourseId = 10, IsRead = false },
                    new Content { Title = "Software Testing Best Practices", CourseId = 11, IsRead = false },
                    new Content { Title = "Database Performance Tuning", CourseId = 12, IsRead = false },
                    new Content { Title = "Ethical Hacking Tools and Techniques", CourseId = 13, IsRead = false },
                    new Content { Title = "Big Data Visualization with D3.js", CourseId = 14, IsRead = false },
                    new Content { Title = "IoT Device Integration and Interoperability", CourseId = 15, IsRead = false },
                    new Content { Title = "Game Development with Unity", CourseId = 16, IsRead = false }
                    };
                    context.Contents.AddRange(contentsToAdd);
                    await context.SaveChangesAsync();
                };




            if (context.ContentTexts.Count() < 1)
            {
                var contentTextsToAdd = new List<ContentText>
                {
                    new ContentText { Title = "Introduction to HTML", SubTitle = "What is HTML?", Paragraph = "HTML is the standard markup language for web pages...", ContentId = 1 },
                    new ContentText { Title = "Working with CSS", SubTitle = "Styling with CSS", Paragraph = "CSS is used to control the style of a web page...", ContentId = 2 },
                    new ContentText { Title = "JavaScript Loops Explained", SubTitle = "For and While Loops", Paragraph = "Loops allow for iteration over data in JavaScript...", ContentId = 3 },
                    new ContentText { Title = "HTML5 Semantic Elements", SubTitle = "Understanding Semantics", Paragraph = "HTML5 introduces new semantic elements...", ContentId = 1 },
                    new ContentText { Title = "CSS Media Queries", SubTitle = "Responsive Design", Paragraph = "Media queries allow for responsive web design...", ContentId = 1 },
                    new ContentText { Title = "JavaScript DOM Manipulation", SubTitle = "Interacting with the DOM", Paragraph = "The DOM represents the structure of an HTML document...", ContentId = 2 },
                    new ContentText { Title = "Data Cleaning and Preprocessing", SubTitle = "Preparing Data for Analysis", Paragraph = "Data cleaning is essential for accurate analysis...", ContentId = 3 },
                    new ContentText { Title = "Network Security Principles", SubTitle = "Protecting Networks", Paragraph = "Network security measures protect against threats...", ContentId = 4 },
                    new ContentText { Title = "AWS EC2 and S3", SubTitle = "Cloud Computing Services", Paragraph = "AWS EC2 and S3 are fundamental cloud services...", ContentId = 5 },
                    new ContentText { Title = "Git and Version Control", SubTitle = "Managing Code Changes", Paragraph = "Git is a popular version control system...", ContentId = 6 },
                    new ContentText { Title = "Machine Learning Algorithms", SubTitle = "Understanding ML Models", Paragraph = "Machine learning algorithms are used for prediction...", ContentId = 7 },
                    new ContentText { Title = "iOS App Development Fundamentals", SubTitle = "Building iOS Apps", Paragraph = "iOS app development involves using Swift...", ContentId = 8 },
                    new ContentText { Title = "Blockchain Consensus Mechanisms", SubTitle = "Ensuring Trust in Blockchain", Paragraph = "Consensus mechanisms maintain the integrity of a blockchain...", ContentId = 9 },
                    new ContentText { Title = "User Interface Design Principles", SubTitle = "Creating User-Friendly Interfaces", Paragraph = "UI design principles focus on usability and aesthetics...", ContentId = 10 },
                    new ContentText { Title = "Software Testing Best Practices", SubTitle = "Ensuring Software Quality", Paragraph = "Effective software testing involves various techniques...", ContentId = 11 },
                    new ContentText { Title = "Database Performance Tuning", SubTitle = "Optimizing Database Queries", Paragraph = "Database performance tuning improves query execution speed...", ContentId = 12 },
                    new ContentText { Title = "Ethical Hacking Tools and Techniques", SubTitle = "Understanding Cybersecurity Threats", Paragraph = "Ethical hacking involves simulating attacks to identify vulnerabilities...", ContentId = 13 },
                    new ContentText { Title = "Big Data Visualization with D3.js", SubTitle = "Creating Interactive Visualizations", Paragraph = "D3.js is a powerful JavaScript library for data visualization...", ContentId = 14 },
                    new ContentText { Title = "IoT Device Integration and Interoperability", SubTitle = "Connecting IoT Devices", Paragraph = "IoT devices must communicate and interact with each other...", ContentId = 15 },
                    new ContentText { Title = "Game Development with Unity", SubTitle = "Creating Interactive Games", Paragraph = "Unity is a popular game development engine...", ContentId = 16 },
                    new ContentText { Title = "Data Visualization with Tableau", SubTitle = "Creating Engaging Visualizations", Paragraph = "Tableau is a powerful data visualization tool...", ContentId = 17 },
                    new ContentText { Title = "Low-Code/No-Code Platform Comparison", SubTitle = "Choosing the Right Platform", Paragraph = "Low-code/no-code platforms streamline application development...", ContentId = 18 },
                    new ContentText { Title = "Cybersecurity Incident Response Case Studies", SubTitle = "Learning from Past Incidents", Paragraph = "Analyzing past incidents helps improve cybersecurity preparedness...", ContentId = 19 },
                    new ContentText { Title = "MLOps with Kubernetes", SubTitle = "Deploying ML Models", Paragraph = "Kubernetes is a container orchestration platform...", ContentId = 20 },
                    new ContentText { Title = "Quantum Computing Algorithms", SubTitle = "Exploring Quantum Computing", Paragraph = "Quantum computing leverages quantum mechanics...", ContentId = 21 },
                    new ContentText { Title = "JavaScript Debugging and Troubleshooting", SubTitle = "Finding and Fixing Errors", Paragraph = "Effective debugging is essential for JavaScript development...", ContentId = 2 },
                    new ContentText { Title = "Data Analysis with Pandas", SubTitle = "Python Data Analysis Library", Paragraph = "Pandas is a powerful Python library for data analysis...", ContentId = 3 },
                    new ContentText { Title = "Network Security Monitoring and Analysis", SubTitle = "Detecting Threats", Paragraph = "Network security monitoring tools help identify threats...", ContentId = 4 },
                    new ContentText { Title = "AWS Serverless Architecture", SubTitle = "Building Scalable Applications", Paragraph = "Serverless architecture allows for on-demand scaling...", ContentId = 5 },
                    new ContentText { Title = "Continuous Delivery Pipelines", SubTitle = "Automating Software Delivery", Paragraph = "Continuous delivery pipelines automate the software delivery process...", ContentId = 6 },
                    new ContentText { Title = "Machine Learning Model Evaluation", SubTitle = "Assessing Model Performance", Paragraph = "Model evaluation metrics measure the accuracy of ML models...", ContentId = 7 },
                    new ContentText { Title = "iOS App Design and User Experience", SubTitle = "Creating User-Friendly Apps", Paragraph = "iOS app design focuses on user experience principles...", ContentId = 8 },
                    new ContentText { Title = "Blockchain Smart Contract Security", SubTitle = "Protecting Smart Contracts", Paragraph = "Smart contract security is crucial for blockchain applications...", ContentId = 9 },
                    new ContentText { Title = "User Research and Testing", SubTitle = "Understanding User Needs", Paragraph = "User research and testing provide valuable insights...", ContentId = 10 },
                    new ContentText { Title = "Software Testing Best Practices", SubTitle = "Ensuring Software Quality", Paragraph = "Effective software testing involves various techniques...", ContentId = 11 },
                    new ContentText { Title = "Database Performance Tuning", SubTitle = "Optimizing Database Queries", Paragraph = "Database performance tuning improves query execution speed...", ContentId = 12 },
                    new ContentText { Title = "Ethical Hacking Tools and Techniques", SubTitle = "Understanding Cybersecurity Threats", Paragraph = "Ethical hacking involves simulating attacks to identify vulnerabilities...", ContentId = 13 },
                    new ContentText { Title = "Big Data Visualization with D3.js", SubTitle = "Creating Interactive Visualizations", Paragraph = "D3.js is a powerful JavaScript library for data visualization...", ContentId = 14 },
                    new ContentText { Title = "IoT Device Integration and Interoperability", SubTitle = "Connecting IoT Devices", Paragraph = "IoT devices must communicate and interact with each other...", ContentId = 15 },
                    new ContentText { Title = "Game Development with Unity", SubTitle = "Creating Interactive Games", Paragraph = "Unity is a popular game development engine...", ContentId = 16 },
                    new ContentText { Title = "Data Visualization with Tableau", SubTitle = "Creating Engaging Visualizations", Paragraph = "Tableau is a powerful data visualization tool...", ContentId = 17 },
                    new ContentText { Title = "Low-Code/No-Code Platform Comparison", SubTitle = "Choosing the Right Platform", Paragraph = "Low-code/no-code platforms streamline application development...", ContentId = 18 },
                    new ContentText { Title = "Cybersecurity Incident Response Case Studies", SubTitle = "Learning from Past Incidents", Paragraph = "Analyzing past incidents helps improve cybersecurity preparedness...", ContentId = 19 },
                    new ContentText { Title = "MLOps with Kubernetes", SubTitle = "Deploying ML Models", Paragraph = "Kubernetes is a container orchestration platform...", ContentId = 20 },
                    new ContentText { Title = "Quantum Computing Algorithms", SubTitle = "Exploring Quantum Computing", Paragraph = "Quantum computing leverages quantum mechanics...", ContentId = 21 },
                    new ContentText { Title = "JavaScript Debugging and Troubleshooting", SubTitle = "Finding and Fixing Errors", Paragraph = "Effective debugging is essential for JavaScript development...", ContentId = 2 },
                    new ContentText { Title = "Data Analysis with Pandas", SubTitle = "Python Data Analysis Library", Paragraph = "Pandas is a powerful Python library for data analysis...", ContentId = 3 },
                    new ContentText { Title = "Network Security Monitoring and Analysis", SubTitle = "Detecting Threats", Paragraph = "Network security monitoring tools help identify threats...", ContentId = 4 }
                };
                context.ContentTexts.AddRange(contentTextsToAdd);
                await context.SaveChangesAsync();
            };



            await SeedRoles(userManager, roleManager);

            await SeedAdminUser(userManager, roleManager);
        }

        private static async Task SeedRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole<int> { Name = "Admin" };
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("Student"))
            {
                var studentRole = new IdentityRole<int> { Name = "Student" };
                await roleManager.CreateAsync(studentRole);
            }
        }

        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            var adminUserName = "admin";
            var adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = "admin@learning.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "111z");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }
    }


}
