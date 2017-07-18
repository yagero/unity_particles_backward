using UnityEngine;
using System.Collections;

public static class ParticleSystemEx
{
    public static Coroutine PlayBackward(this ParticleSystem self, MonoBehaviour mb, bool withChildren = true)
    {
        Debug.Assert(self);
        return mb.StartCoroutine(CoPlayBackward(self, withChildren));
    }

    public static Coroutine PlayBackward(this ParticleSystem self, MonoBehaviour mb, float maxLifeTime, bool withChildren = true)
    {
        Debug.Assert(self);
        return mb.StartCoroutine(CoPlayBackward(self, maxLifeTime, withChildren));
    }

    public static IEnumerator CoPlayBackward(this ParticleSystem self, bool withChildren = true)
    {
        Debug.Assert(self);
        yield return self.CoPlayBackward(self.GetMaxLifeTime(withChildren), withChildren);
    }

    public static IEnumerator CoPlayBackward(this ParticleSystem self, float maxLifeTime, bool withChildren = true)
    {
        Debug.Assert(self);
        Debug.Assert(maxLifeTime > 0f);
        self.Stop(withChildren);
        self.Clear(withChildren);
        self.SetUseAutoRandomSeed(false, withChildren);
        self.Play(withChildren);

        float time = maxLifeTime;
        while (time > 0f)
        {
            self.Simulate(time, withChildren, true);
            time -= Time.deltaTime;
            yield return null;
        }

        self.Simulate(0f, withChildren, true);
    }

    public static void SetUseAutoRandomSeed(this ParticleSystem self, bool value, bool withChildren = true)
    {
        Debug.Assert(self);
        if (!withChildren) {
            self.useAutoRandomSeed = value;
        }
        else {
            var allps = self.GetComponentsInChildren<ParticleSystem>();
            foreach (var ps in allps)
                ps.useAutoRandomSeed = value;
        }
    }

    public static float GetMaxLifeTime(this ParticleSystem self, bool withChildren = true)
    {
        Debug.Assert(self);
        var maxLifeTime = float.MinValue;

        if (!withChildren)
        {
            maxLifeTime = self.main.startLifetime.GetMaxValue();
        }
        else
        {
            var allps = self.GetComponentsInChildren<ParticleSystem>();
            foreach (var ps in allps)
                maxLifeTime = Mathf.Max(maxLifeTime, ps.main.startLifetime.GetMaxValue());
        }

        return maxLifeTime;
    }

    public static float GetMaxValue(this ParticleSystem.MinMaxCurve self)
    {
        switch (self.mode)
        {
            case ParticleSystemCurveMode.Constant:
                return self.constant;
            case ParticleSystemCurveMode.TwoConstants:
                return self.constantMax;
            case ParticleSystemCurveMode.Curve:
            case ParticleSystemCurveMode.TwoCurves:
            default:
                return self.curveMax.GetMaxValue() * self.curveMultiplier;
        }
    }

    public static float GetMaxValue(this AnimationCurve self)
    {
        float maxValue = float.MinValue;
        for (int i = 0; i < self.length; ++i)
            maxValue = Mathf.Max(maxValue, self.keys[i].value);
        return maxValue;
    }
}
