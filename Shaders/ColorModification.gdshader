shader_type canvas_item;

uniform vec4 original_color_1: source_color;
uniform vec4 original_color_2: source_color;
uniform vec4 original_color_3: source_color;

uniform vec4 replace_color_1: source_color;
uniform vec4 replace_color_2: source_color;
uniform vec4 replace_color_3: source_color;

const float PRECISION = 0.1;

vec4 replace_color(vec4 color) {
	vec4 original_colors[3] = vec4[3](original_color_1, original_color_2, original_color_3);
	vec4 replace_colors[3] = vec4[3](replace_color_1, replace_color_2, replace_color_3);

	for(int i = 0; i < original_colors.length(); ++i) {
		if (distance(color, original_colors[i]) <= PRECISION) {
			color = replace_colors[i];
		}
	}

	return color;
}

void fragment() {
	vec4 color = texture(TEXTURE, UV);
	COLOR = replace_color(color);
}
