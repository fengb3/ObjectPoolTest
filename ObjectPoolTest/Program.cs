// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;

Console.WriteLine("Hello, World!");

var obj = ObjPool<ObjectThatCanBeReused>.Get();

obj.Name = "田所浩二";
obj.Id   = 114514;

Console.WriteLine($"姓名: {obj.Name}, Id: {obj.Id}, 年龄: {obj.Age}"); // 在回收前都可以访问属性，赋值和取值都可以

ObjPool<ObjectThatCanBeReused>.Reuse(obj); // 回收后则不能访问属性

Console.WriteLine($"姓名: {obj.Name}, Id: {obj.Id}"); // 这一行会抛出异常


public class ReusedObjectAttribute : Attribute;

public abstract class ReusedObject
{
	/// <summary>
	/// 对象是否被回收
	/// </summary>
	public bool IsRecycled;
}

[ReusedObject]
public partial class ObjectThatCanBeReused
{
	private int    _id      = 0;
	private string _name    = "";
	private int    _age     = 24;
	private string _address = "";
}


public static class ObjPool<T> where T : ReusedObject
{
	static readonly ConcurrentQueue<T> _pool = new();

	static ObjPool()
	{
		var attributes = typeof(T).GetCustomAttributes(typeof(ReusedObjectAttribute), true);

		if (!attributes.Any())
			throw new Exception($"{typeof(T).Name} is not a recycle object.");
	}

	public static T Get()
	{
		if (!_pool.TryDequeue(out var item))
		{
			item = Activator.CreateInstance<T>();
		}

		item.IsRecycled = false;

		return item;
	}

	public static void Reuse(T item)
	{
		item.IsRecycled = true;
		
		_pool.Enqueue(item);
	}
}