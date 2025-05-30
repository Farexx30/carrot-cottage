shader_type canvas_item;

uniform float shake_intensity = 0.0;
uniform float shake_speed = 30.0;
uniform float shake_height = 0.0;

void vertex() {
	vec2 shake = vec2(0.0);

	if(VERTEX.y < shake_height) {
		shake.x = sin(TIME * shake_speed + VERTEX.y) * shake_intensity;
	}

	VERTEX.xy += shake;
}
