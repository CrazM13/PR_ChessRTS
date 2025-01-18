using Godot;
using System;

public static class PieceTypeUtils {

	public static PieceTypes Max(PieceTypes left, PieceTypes right) {
		if (left < right) {
			return right;
		} else {
			return left;
		}
	}

}
