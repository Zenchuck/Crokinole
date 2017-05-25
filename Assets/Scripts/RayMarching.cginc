half3 doTunnel( sampler2D sample, in half2 uv, in half time ) 
{
//    // get polar coordinates    
//    float g = atan2( uv.y, uv.x );
//    float b = atan2( uv.y, abs(uv.x) );
//    float r = length( uv );
//    
//    // pack and animate    

//
//    // fetch from texture    
//    return tex2D( sample, uvL, ddx(uvR), ddy(uvR)).xyz;
    // get polar coordinates 
    float g = atan2(uv.y,uv.x);//*.5+.5;
    float r = length( uv );
    
    // pack and animate    
    uv = half2( .01/r + time, g/3.1415927 );

    // fetch from texture    
    return tex2D( sample, uv ).xyz;
}

float hash( float n ) 
{
	return frac(sin(n)*43758.5453123);
}

half noise( half2 x ) 
{
	half2 p = floor(x);
	half2 f = frac(x);

	f = f*f*(3.0-2.0*f);

	float n = p.x + p.y*57.0;

	half q = lerp(lerp( hash(n+  0.0), hash(n+  1.0),f.x), lerp( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y);
	return q;
}

half2 rotate(half2 uv,half t)
{
	half2x2 m = half2x2(cos(t),-sin(t),sin(t),cos(t));
	half2 ruv = mul(m,uv);
	return ruv;
}

//half fbm( half2 p)
//{
//	half f = 0;
//	f+= .5*noise(p); p*=2.02;
//	mul(m,p);
//	f+= .25*noise(p); p*=2.03;
//	mul(m,p);
//	f+= .125*noise(p); p*=2.01;
//	mul(m,p);
//	f+= .0625*noise(p);
//	f/= .9375;
//	return f;
//}

half square(half2 uv,half4 bounds)
{
	half y  = 0;
	half x  = 0;
	y = smoothstep(bounds.x,bounds.x,uv.y)*
		1-smoothstep(bounds.x+bounds.y,bounds.x+bounds.y,uv.y);
	x =	smoothstep(bounds.z,bounds.z,uv.x)*
		1-smoothstep(bounds.z+bounds.w,bounds.z+bounds.w,uv.x);
	return x*y;
}

half4 drawBB(half2 uv,half4 bounds,half4 tex)
{
	half y  = 0;
	half x  = 0;
	x = smoothstep(bounds.x,bounds.x,uv.x)*
		1-smoothstep(bounds.x+bounds.z,bounds.x+bounds.z,uv.x);
	y =	smoothstep(bounds.y,bounds.y,uv.y)*
		1-smoothstep(bounds.y+bounds.w,bounds.y+bounds.w,uv.y);
	fixed mask = x*y;
	return tex*mask;
}
