using lap2_ef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           
            LibraryDbContext context = new LibraryDbContext();
            

            
             
                // 1. إضافة كائن واحد (Insert a single object)
                Console.WriteLine("\n=== إضافة كائن واحد ===");
                var author = new Author { Name = "أحمد خالد توفيق" };
                context.Authors.Add(author);
                context.SaveChanges();
                Console.WriteLine($"تم إضافة المؤلف: {author.Name} بنجاح!");

                // 2. إضافة قائمة من الكائنات (Insert a list of objects)
                Console.WriteLine("\n=== إضافة قائمة من الكتب ===");
                var books = new List<Book>
                {
                    new Book { Title = "يوتوبيا", AuthorId = author. Id },
                    new Book { Title = "السجيل", AuthorId = author.Id }
                };
                context.Books.AddRange(books);
                context.SaveChanges();
                Console.WriteLine("تم إضافة الكتب بنجاح!");

                // 3. إضافة باستخدام خصائص التنقل (Insert using navigation properties)
                Console.WriteLine("\n=== إضافة باستخدام خصائص التنقل ===");
                var newAuthor = new Author
                {
                    Name = "نجيب محفوظ",
                    Books = new List<Book>
                    {
                        new Book { Title = "الثلاثية" },
                        new Book { Title = "أولاد حارتنا" }
                    }
                };
                context.Authors.Add(newAuthor);
                context.SaveChanges();
                Console.WriteLine("تم إضافة المؤلف والكتب باستخدام خصائص التنقل!");

                // 4. تحديث كائن (Update objects)
                Console.WriteLine("\n=== تحديث كائن ===");
                var authorToUpdate = context.Authors.FirstOrDefault(a => a.Name == "أحمد خالد توفيق");
                if (authorToUpdate != null)
                {
                    authorToUpdate.Name = "د. أحمد خالد توفيق";
                    context.SaveChanges();
                    Console.WriteLine("تم تحديث اسم المؤلف!");
                }

                // 5. استخدام EntityState
                Console.WriteLine("\n=== استخدام EntityState ===");
                var bookToUpdate = context.Books.FirstOrDefault(b => b.Title == "يوتوبيا");
                if (bookToUpdate != null)
                {
                    bookToUpdate.Title = "يوتوبيا - الطبعة الجديدة";
                    context.Entry(bookToUpdate).State = EntityState.Modified;
                    context.SaveChanges();
                    Console.WriteLine("تم تحديث الكتاب باستخدام EntityState!");
                }

                // 6. حذف قائمة من الكائنات (Delete a list of objects)
                Console.WriteLine("\n=== حذف قائمة من الكتب ===");
                var booksToDelete = context.Books.Where(b => b.AuthorId == author.Id).ToList();
                context.Books.RemoveRange(booksToDelete);
                context.SaveChanges();
                Console.WriteLine("تم حذف الكتب بنجاح!");

                // 7. Eager Loading
                Console.WriteLine("\n=== Eager Loading ===");
                var authorsWithBooks = context.Authors
                    .Include(a => a.Books)
                    .ToList();
                foreach (var a in authorsWithBooks)
                {
                    Console.WriteLine($"المؤلف: {a.Name}, عدد الكتب: {a.Books.Count}");
                }

                // 8. Lazy Loading
                Console.WriteLine("\n=== Lazy Loading ===");
                var lazyAuthor = context.Authors.FirstOrDefault(a => a.Name == "نجيب محفوظ");
                if (lazyAuthor != null)
                {
                    Console.WriteLine($"المؤلف: {lazyAuthor.Name}, عدد الكتب: {lazyAuthor.Books.Count}");
                }
            }
        }
    }
