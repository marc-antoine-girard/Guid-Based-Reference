using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

namespace ShackLab.Editor.Tests
{
    public class GuidReferenceTests
    {
        // Tests - make a new GUID
        // duplicate it
        // make it a prefab
        // delete it
        // reference it
        // dereference it

        string prefabPath;
        GuidComponent guidBase;
        GameObject prefab;
        GuidComponent guidPrefab;

        [OneTimeSetUp]
        public void Setup()
        {
            prefabPath = "Assets/TemporaryTestGuid.prefab";

            guidBase = CreateNewGuid();
            prefab = PrefabUtility.SaveAsPrefabAsset(guidBase.gameObject, prefabPath);

            guidPrefab = prefab.GetComponent<GuidComponent>();
        }

        private GuidComponent CreateNewGuid()
        {
            GameObject newGo = new GameObject("GuidTestGO");
            return newGo.AddComponent<GuidComponent>();
        }

        [UnityTest]
        public IEnumerator GuidCreation()
        {
            GuidComponent guid1 = guidBase;
            GuidComponent guid2 = CreateNewGuid();

            Assert.AreNotEqual(guid1.GetGuid(), guid2.GetGuid());

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuidDuplication()
        {
            LogAssert.Expect(LogType.Warning,
                "Guid Collision Detected while creating GuidTestGO(Clone).\nAssigning new Guid.");

            GuidComponent clone = Object.Instantiate(guidBase);

            Assert.AreNotEqual(guidBase.GetGuid(), clone.GetGuid());

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuidPrefab()
        {
            Assert.AreNotEqual(guidBase.GetGuid(), guidPrefab.GetGuid());
            Assert.AreEqual(guidPrefab.GetGuid(), System.Guid.Empty);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuidPrefabInstance()
        {
            GuidComponent instance = Object.Instantiate(guidPrefab);
            Assert.AreNotEqual(guidBase.GetGuid(), instance.GetGuid());
            Assert.AreNotEqual(instance.GetGuid(), guidPrefab.GetGuid());

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuidValidReference()
        {
            GuidReference reference = new GuidReference(guidBase);
            Assert.AreEqual(reference.gameObject, guidBase.gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuidInvalidReference()
        {
            GuidComponent newGuid = CreateNewGuid();
            GuidReference reference = new GuidReference(newGuid);
            Object.DestroyImmediate(newGuid);

            Assert.IsNull(reference.gameObject);

            yield return null;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset(prefabPath);
        }
    }
}