using UnityEngine;
using System.Collections;

public class InterfaceTemlpateTest : Freme, Meme, Scream, TemplateClass<Freme>
{
	
}

public interface Meme
{
}

public interface Scream
{}

public class Freme
{}

public class Fumbleme
{}

public interface TemplateClass<T>
{

}