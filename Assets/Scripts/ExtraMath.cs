using UnityEngine;


    public static class ExtraMath
    {
    //Puede usarse para rotar en un angulo fijo siempre si pasas el mismo minAngle que maxAngle
    public static Vector3 GetRotatedVectorInRandomAngle(float minAngle, float maxAngle, float magnitude, Vector3 starterVector, Vector3 rotationAxis) //Solo apto para plano, osea no consideramos la posicion Y
    {
        //Si startervector es paralelo a (0,1,0) no anda, hay q revisar eso
        //Tampoco sirve para (0,0,0)
        Vector3 auxDirection = starterVector;
        auxDirection.y = 0;
        if (auxDirection.magnitude == 0)
            Debug.Log("El vector que pasaste no es apto para trabajar");

        float angle = Random.Range(minAngle, maxAngle);
        Quaternion rotation = Quaternion.AngleAxis(angle, rotationAxis);

        Vector3 finalVectorDirection = (rotation * auxDirection).normalized;
        Vector3 finalVector = finalVectorDirection * magnitude;
        
        return finalVector;

    }
}

