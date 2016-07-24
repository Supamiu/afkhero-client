using UnityEngine;
using System.Collections;

public class ProblemTest : MonoBehaviour {

	void Start () {
        Foo testInstance = new Foo(new Child());
        Foo test = JsonUtility.FromJson<Foo>(JsonUtility.ToJson(testInstance));
        Debug.Log(test.bar.GetType());
	}

    [System.Serializable]
    public class Bar
    {
        public string name = "bar";
    }
    [System.Serializable]
    public class Child : Bar
    {
        public string otherName = "Child";
    }

    [System.Serializable]
    public class Foo
    {
        public Bar bar;

        public Foo(Bar bar)
        {
            this.bar = bar;
        }
    }
}
