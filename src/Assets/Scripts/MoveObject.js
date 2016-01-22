#pragma strict

private var xmax : float = 2.0;
private var xmin : float = -2.0;
private var zmax : float = 3.5;
private var zmin : float = 1.4;
private var	v_current : Vector3;
private var vx : float;
private var vy : float;
private var vz : float;
private var x : float;
private var z : float;
private var ax_max : float = 0.6;
private var ax_min : float = -0.6;
private var az_max : float = -0.5;
private var az_min : float = -1.0;
private var mx : float = 0.0;
private var mz : float = 0.0;
private var myTime : float = 0.0f;
private var CTE_TIME : float = 0.150;

function ValidData (max:float,min:float,d:float)
{
	if(max < d)
		return max;
	if(min > d)
		return min;
					
	return d;
}

function Start () {
	Screen.sleepTimeout = SleepTimeout.NeverSleep;
	v_current = Input.acceleration;
	vx = v_current.x;
	vz = v_current.z;
	vy = transform.position.y;
	vx = ValidData(ax_max,ax_min,vx);
	vz = ValidData(az_max,az_min,vz);
	mx = (xmax-xmin)/(ax_max-ax_min);
	mz = (zmin-zmax)/(az_max-az_min);
	x = mx*(vx-ax_min) + xmin;
	z = mz*(vz-az_min) + zmax;
}

function Update () 
{
	transform.position.x = x;	
	transform.position.z = z;
	transform.position.y = vy;
	
	myTime = myTime + Time.deltaTime;
	
	if(myTime > CTE_TIME)
	{
		v_current = Input.acceleration;
		vx = v_current.x;
		vz = v_current.z;
		vx = ValidData(ax_max,ax_min,vx);
		vz = ValidData(az_max,az_min,vz);
		x = mx*(vx-ax_min) + xmin;
		z = mz*(vz-az_min) + zmax;
		myTime = 0.0;
	}
}